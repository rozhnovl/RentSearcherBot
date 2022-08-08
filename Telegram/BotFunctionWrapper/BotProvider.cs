using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Azure.Storage.Blobs;
using Polly;

namespace BotFunctionWrapper
{
    public abstract class BotProvider<T> where T : new()
    {
        private readonly string botToken;
        private readonly BlobContainerClient cloudBlobContainer;
        protected readonly ILogger Log;

        protected BotProvider(string botToken, BlobContainerClient cloudBlobContainer, ILogger log)
        {
            this.botToken = botToken;
            this.cloudBlobContainer = cloudBlobContainer;
            this.Log = log;
        }

        public async Task<BotUserInfo[]> GetSubscribedUsers(BlobContainerClient blobContainer)
        {
            return await GetUserInfos(blobContainer).Select(ui => ui.Item2).ToArrayAsync();
        }

        public async Task ExecuteForEachUserAsync(Func<BotUserInfo, Task> action)
        {
            var execTasks = new List<Task>();
            await foreach (var userInfo in GetUserInfos(cloudBlobContainer))
            {
                execTasks.Add(WrapLogSaveForSingleUser(userInfo, action));
            }

            await Task.WhenAll(execTasks);
        }

        public async Task InitInternal()
            => await InitInternal(Environment.GetEnvironmentVariable("apiKey"),
                Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME"));

        public async Task InitInternal(string apiKey, string webHookUrl)
        {
            Log.LogInformation($"InitInternal called with params: apikey='{apiKey}' webHookUrl='{webHookUrl}'");
            await cloudBlobContainer.CreateIfNotExistsAsync();
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            if (string.IsNullOrWhiteSpace(webHookUrl))
                throw new ArgumentNullException(nameof(webHookUrl));
            var telegramClient = GetBotClient();
            await telegramClient.DeleteWebhookAsync();
            await telegramClient.SetMyCommandsAsync(
                GetEditSettingsCommands().Select(c => c.GetBotCommand)
                    .Union(GetBotCommands().Select(command => new BotCommand()
                        { Command = command.command, Description = command.description })));
            await telegramClient.SetWebhookAsync(
                webHookUrl + $"/api/TelegramBotCallback?code={apiKey}",
                allowedUpdates: new[]
                {
                    UpdateType.Message,
                    UpdateType.CallbackQuery,
                });
            var webHookInfo = await telegramClient.GetWebhookInfoAsync();
            Log.LogInformation("Current Webhook info: " + JsonSerializer.Serialize(webHookInfo));
        }
        
        protected virtual IEnumerable<(string command, string description)> GetBotCommands()
        {
            yield return ("test", "temp description for command");
        }

        protected virtual IEnumerable<UserChangingCommand> GetEditSettingsCommands() =>
            Enumerable.Empty<UserChangingCommand>();

        protected ITelegramBotClient GetBotClient()
        {
            var rateLimit = Policy.RateLimitAsync(30, TimeSpan.FromSeconds(1));
            var retry = Policy.Handle<Exception>()
                .WaitAndRetryAsync(new double[] { 1, 3, 7, 11, 19 }.Select(TimeSpan.FromSeconds));
            return new RetryingTelegramBotClient(new TelegramBotClient(botToken),
                Policy.WrapAsync(rateLimit, retry));
        }
        
        public async Task TelegramBotCallbackInternal(string requestBody)
        {
            Log.LogInformation("Got an incoming request from Telegram: " + requestBody);
            try
            {
                var updates = Newtonsoft.Json.JsonConvert.DeserializeObject<Update>(requestBody);
                if (updates.CallbackQuery != null)
                {
                    await ProcessCallbackQuery(updates.CallbackQuery);
                }
                if (updates.Message != null)
                {
                    var message = updates.Message;
                    Log.LogInformation("incoming message text: " + message.Text);

                    var telegramClient = GetBotClient();
                    var userInfo = await GetUserInfo(message.Chat.Id, message.From.Username);
                    try
                    {
                        var commandProcessor =
                            GetEditSettingsCommands().FirstOrDefault(c => c.MatchRequest(message.Text));
                        
                        (bool shouldSave, string responseMessage) =
                            commandProcessor != null
                            ? commandProcessor.Apply(message.Text, userInfo)
                            :
                            message.Text switch
                        {
                            _ => await ProcessMessageInternal(message.Text, userInfo),
                        };

                        await telegramClient.SendTextMessageAsync(message.Chat.Id, responseMessage);
                        if (shouldSave)
                            await SaveUserInfo(cloudBlobContainer, userInfo);
                    }
                    catch (UnknownCommandException e)
                    {
                        Log.LogError(e, "Unknown command received");
                        await telegramClient.SendTextMessageAsync(message.Chat.Id, "Unknown command received");
                    }
                }
            }
            catch (Exception e)
            {
                Log.LogError(e, "Failed to process message");
                throw;
            }
        }

        protected abstract Task ProcessCallbackQuery(CallbackQuery query);
        protected async Task<BotUserInfo> GetUserInfo(long chatId, string username)
        {
            var settingsName = chatId + ".json";
            var blobReference = cloudBlobContainer.GetBlobClient("monitoredUsers/" + settingsName);
            if (!await blobReference.ExistsAsync())
            {
                return new BotUserInfo(chatId, username);
            }

            return JsonSerializer.Deserialize<BotUserInfo>((await blobReference.DownloadContentAsync()).Value.Content.ToString());
        }

        protected async Task SaveUserInfo(BlobContainerClient blobContainer, BotUserInfo userInfo)
        {
            var settingsName = userInfo.ChatId + ".json";
            var blobReference = blobContainer.GetBlobClient("monitoredUsers/" + settingsName);
            await blobReference
                .UploadAsync(new BinaryData(JsonSerializer.Serialize(userInfo)), true);
        }

        protected abstract Task<(bool shouldChangeState, string responseMessage)> ProcessMessageInternal(string message,
            BotUserInfo userInfo);

        private async IAsyncEnumerable<(BlobClient, BotUserInfo)> GetUserInfos(
            BlobContainerClient blobContainer)
        {
            await foreach (var bs in blobContainer.GetBlobsByHierarchyAsync(prefix: "monitoredUsers"))
                yield return (blobContainer.GetBlobClient(bs.Blob.Name), JsonSerializer.Deserialize<BotUserInfo>((await blobContainer.GetBlobClient(bs.Blob.Name).DownloadContentAsync()).Value.Content.ToString()));
        }

        private async Task WrapLogSaveForSingleUser((BlobClient, BotUserInfo) userInfo,
            Func<BotUserInfo, Task> action)
        {
            try
            {
                await action(userInfo.Item2);
                //TODO here we should check whether userInfo was modified
                await userInfo.Item1.UploadAsync(new BinaryData(JsonSerializer.Serialize(userInfo.Item2)), true);
            }
            catch (Exception e) when (e.Message.Contains("Bot was blocked by the user"))
            {
                //TODO stop messaging to the user.
                await userInfo.Item1.DeleteAsync();
            }
            catch (Exception e)
            {
                Log.LogError(e, $"Failed to process for user {userInfo.Item2.Username}({userInfo.Item2.ChatId}): {e.Message}");
            }
        }

        public class BotUserInfo
        {
            public long ChatId { get; set; }
            public string Username { get; set; }
            public T Data { get; set; }

            public BotUserInfo()
            {

            }

            internal BotUserInfo(long chatId, string username)
            {
                ChatId = chatId;
                Data = new T();
            }
        }

        public record UserChangingCommand(string PropertyName, string Description, Func<BotUserInfo, string, string> changeFunc,
            UserChangingCommand.EmptyCommandArgumentBehavior EmptyArgumentBehavior)
        {
            public static UserChangingCommand NullifyEmpty(string propertyName, string description, Func<BotUserInfo, string, string> changeFunc) =>
                new UserChangingCommand(propertyName, description, changeFunc, EmptyCommandArgumentBehavior.SetNull);

            public static UserChangingCommand KeepEmpty(string propertyName, string description, Func<BotUserInfo, string, string> changeFunc) =>
                new UserChangingCommand(propertyName, description, changeFunc, EmptyCommandArgumentBehavior.KeepEmpty);

            public static UserChangingCommand Require(string propertyName, string description, Func<BotUserInfo, string, string> changeFunc) =>
                new UserChangingCommand(propertyName, description, changeFunc, EmptyCommandArgumentBehavior.Require);

            public bool MatchRequest(string message)
            {
                return message.StartsWith("/set" + PropertyName, StringComparison.InvariantCultureIgnoreCase);
            }

            public (bool, string) Apply(string message, BotUserInfo userInfo)
            {
                var value = message.Substring(("/set" + PropertyName).Length).Trim();
                if (string.IsNullOrEmpty(value))
                {
                    switch (EmptyArgumentBehavior)
                    {
                        case EmptyCommandArgumentBehavior.KeepEmpty:
                            break;
                        case EmptyCommandArgumentBehavior.SetNull:
                            value = null;
                            break;
                        case EmptyCommandArgumentBehavior.Skip:
                            return (false, "value passed is empty, nothing changed");
                        case EmptyCommandArgumentBehavior.Require:
                            return (false, "value is required but appear to be empty");
                        default:
                            throw new ArgumentOutOfRangeException(nameof(EmptyArgumentBehavior), EmptyArgumentBehavior, "unhandled behavior enum value");
                    }
                }

                var response = changeFunc(userInfo, value);
                return (true, response);
            }

            public BotCommand GetBotCommand => new()
                { Command = "/set" + PropertyName.ToLower(), Description = Description };

            public enum EmptyCommandArgumentBehavior
            {
                SetNull, KeepEmpty, Skip, Require,
            }
        }

    }

    public class UnknownCommandException : Exception
    {
        public string Message { get; private set; }
        public UnknownCommandException(string message)
        {
            Message = message;
        }
    }
}