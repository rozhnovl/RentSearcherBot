using Azure.Storage.Blobs;
using Azure.Storage.Queues.Models;
using BotFunctionWrapper;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramMessageListener
{
    public class TelegramBotFunctions : BotProvider
        <SearchSubscriberInfo>
    {
        private readonly BlobContainerClient messageIdGroupsContainer;
        private readonly string translationServiceKey;

        public TelegramBotFunctions(BlobServiceClient cloudBlobClient,
            ILogger<TelegramBotFunctions> log) : base(
            Environment.GetEnvironmentVariable("botToken"), cloudBlobClient.GetBlobContainerClient("telegramuserinfos"), log)
        {
            this.messageIdGroupsContainer = cloudBlobClient.GetBlobContainerClient("messageidgroupscontainer");
            this.translationServiceKey = Environment.GetEnvironmentVariable("TranslationServiceKey");
        }

        [FunctionName("Init")]
        public async Task Init([HttpTrigger] HttpRequest request)
        {
            await messageIdGroupsContainer.CreateIfNotExistsAsync();
            await base.InitInternal();
        }

        [FunctionName("TelegramBotCallback")]
        public async Task TelegramBotCallback([HttpTrigger] HttpRequest request)
            => await base.TelegramBotCallbackInternal(await request.ReadAsStringAsync());

        [FunctionName("OrderCreationCallback")]
        public async Task OrderCreationMessageReceived([QueueTrigger("estatesaddedqueue")] QueueMessage message)
        {
            var request = JsonSerializer.Deserialize<EstateListing>(message.Body.ToString());
            var translatedDescription = new Lazy<Task<string>>(() => new TranslatorClient(Log).TranslateTextRequest(
                translationServiceKey,
                "/translate?api-version=3.0&from=cs&to=ru",
                request.Details?.Description));
            var client = GetBotClient();
            await ExecuteForEachUserAsync(async ui =>
            {
                if (!ShouldUserGetListing(ui.Data, request))
                    return;
                var description = await translatedDescription.Value;
                var messageText = $@"
A new posting has been added on SReality:
{request.name}
{request.locality}
💵 {request.price}";
                if (request.Details.hasElevator.HasValue || request.Details.floor!=null)
                {
                    messageText +=
                        $"\n\nFloor: {request.Details.floor}, {(request.Details.hasElevator.HasValue ? (request.Details.hasElevator.Value ? "Elevator: ✔" : "Elevator: ❌") : "")}";
                }
                var mediaMessages = await client.SendMediaGroupAsync(ui.ChatId,
                    (request.Details.images?.Take(3) ?? Enumerable.Empty<string>()).Select((img, i) =>
                        new InputMediaPhoto(new InputMedia(img))
                        {
                            Caption = i == 0 ? RemoveMeaninglessText(description).Truncate(1023) : null,
                        }).ToArray(), disableNotification: true);
                var descriptionMessage = await client.SendTextMessageAsync(ui.ChatId, messageText,
                    replyMarkup: new InlineKeyboardMarkup(new[]
                    {
                        InlineKeyboardButton.WithUrl("💻", request.DetailsUrl),
                        InlineKeyboardButton.WithCallbackData("✔", $"Confirm;{request.hash_id}"),
                        InlineKeyboardButton.WithCallbackData("❌", $"Reject;{request.hash_id}"),
                    }));
                await messageIdGroupsContainer.UploadBlobAsync($"{ui.ChatId}:{request.hash_id}",
                    new BinaryData(string.Join(":",
                        mediaMessages.Union(new[] { descriptionMessage }).Select(m => m.MessageId))));
            });
        }

        private bool ShouldUserGetListing(SearchSubscriberInfo userInfo, EstateListing listing)
        {
            if (userInfo.MaxPrice.HasValue && listing.price > userInfo.MaxPrice)
                return false;
            if (userInfo.MinRoomNumber.HasValue && listing.RoomNumber < userInfo.MinRoomNumber)
                return false;
            return true;
        }

        private static Dictionary<string, int> MeaningWeight = new Dictionary<string, int>()
        {
            { "Класс энергопотребления", -100 },
            { "энергетический класс", -100 },
            { "Категория энергии", -100 },
            { "животн", -1 },
            { "трамвай", -1 },
            { "депозит", 10 },
            { "м2", 10 },

            { "лифт", 10 },
            { "мебл", 10 },
            { "мебел", 10 },
            { "хранен", 5 },
            { "парк", 10 },

            { "CZK", 10 },
            { "потреблен", 10 },
            { "комис", 10 },
            { "этаж", 10 },
            { "близост", -10 },
            { "располож", -10 },
            { "администрат", -10 },
            { "окрестн", -10 },
            { "студент", -10 },
        };

        public string RemoveMeaninglessText(string input)
        {
            var result = new StringBuilder();
            string[] sentences = Regex.Split(input, @"(?<=[\.!\?])\s+");
            int remainedSentencesCount = 0;
            foreach (var s in sentences)
            {
                if (s.EndsWith('?') || s.EndsWith("? "))
                    continue;
                var weight = MeaningWeight.Where(kv => s.Contains(kv.Key, StringComparison.InvariantCultureIgnoreCase))
                    .Sum(kv => kv.Value);
                if (weight < 0)
                    continue;
                result.Append(s);
                remainedSentencesCount++;
            }

            Log.LogInformation($"Shortened text from {sentences.Length} to {remainedSentencesCount} sentences");
            return result.ToString();
        }

        protected override async Task ProcessCallbackQuery(CallbackQuery query)
        {
            Log.LogInformation("Processing callback data: " + query.Data);
            var splitted = query.Data.Split(";");
            var client = GetBotClient();
            var userInfo = await GetUserInfo(query.Message.Chat.Id, query.From.Username);
            switch (splitted[0])
            {
                case "Confirm":
                    //await  client.EditMessageReplyMarkupAsync(query.Message.Chat.Id, query.Message.MessageId, InlineKeyboardMarkup.Empty());
                    var httpClient = new HttpClient();
                    try
                    {
                        var content = JsonContent.Create(new
                        {
                            hash_id = long.Parse(splitted[1]),
                            name = userInfo.Data.Name.Trim(),
                            phone = userInfo.Data.Phone.Trim(),
                            email = userInfo.Data.Email.Trim(),
                            text = userInfo.Data.EmailTemplate.Trim(),
                            agree_to_terms = true
                        });
                        var response =
                            await httpClient.PostAsync("https://www.sreality.cz/api/en/v2/email-query", content);
                        Log.LogInformation("HttpCode from SReality: " + response.StatusCode + ". Content: " +
                                           await response.Content.ReadAsStringAsync());
                        response.EnsureSuccessStatusCode();
                        await client.EditMessageReplyMarkupAsync(query.Message.Chat.Id, query.Message.MessageId,
                            replyMarkup: new InlineKeyboardMarkup(
                                query.Message.ReplyMarkup.InlineKeyboard
                                    .SelectMany(kb =>
                                        kb.Where(b => !(b.CallbackData?.Contains("Confirm") ?? false)))));
                    }
                    catch (Exception e)
                    {
                        Log.LogWarning("Message edition failed: " + e.ToString());
                    }

                    break;
                case "Reject":
                    try
                    {
                        if (await messageIdGroupsContainer.GetBlobClient($"{query.Message.Chat.Id}:{splitted[1]}")
                                .ExistsAsync())
                        {
                            var messageIds =
                                (await messageIdGroupsContainer.GetBlobClient($"{query.Message.Chat.Id}:{splitted[1]}")
                                    .DownloadContentAsync()).Value.Content.ToString().Split(":");
                            foreach (var messageId in messageIds)
                                await client.DeleteMessageAsync(query.Message.Chat.Id, int.Parse(messageId));
                        }
                        else
                            await client.DeleteMessageAsync(query.Message.Chat.Id, query.Message.MessageId);
                    }
                    catch (Exception e)
                    {
                        Log.LogWarning("Message deletion failed: " + e.ToString());
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException("QueryCommand", $"Unknown query command '{splitted[0]}'");
            }
        }

        protected override Task<(bool shouldChangeState, string responseMessage)> ProcessMessageInternal(
            string message, BotUserInfo userInfo)
        {
            return message switch
            {
                { } t when t.StartsWith("/start", StringComparison.InvariantCultureIgnoreCase)
                    => Task.FromResult((true, GetWelcomeMessage(userInfo))),
                _ => throw new UnknownCommandException(message),
            };
        }

        protected override IEnumerable<(string command, string description)> GetBotCommands() =>
            Enumerable.Empty<(string, string)>();

        protected override IEnumerable<UserChangingCommand> GetEditSettingsCommands()
        {
            yield return UserChangingCommand.KeepEmpty("phone", "saves your phone", WrapChangeWithDescription((ui, val) => ui.Data.Phone = val));
            yield return UserChangingCommand.KeepEmpty("email", "saves your email", WrapChangeWithDescription((ui, val) => ui.Data.Email = val));
            yield return UserChangingCommand.Require("name", "saves your name", WrapChangeWithDescription((ui, val) => ui.Data.Name = val));
            yield return UserChangingCommand.KeepEmpty("response", "saves message which will be sent to property owners", WrapChangeWithDescription((ui, val) => ui.Data.EmailTemplate = val));
            yield return UserChangingCommand.NullifyEmpty("maxprice", "filters maximum price (without utilities)", WrapChangeWithDescription((ui, val) => ui.Data.MaxPrice = val != null ? int.Parse(val) : null));
            yield return UserChangingCommand.NullifyEmpty("minrooms", "filters minimum rooms number", WrapChangeWithDescription((ui, val) => ui.Data.MinRoomNumber = val != null ? int.Parse(val) : null));
        }

        private string GetWelcomeMessage(BotUserInfo ui) => "TODO";
        private Func<BotUserInfo, string, string> WrapChangeWithDescription(Action<BotUserInfo, string> changeFunc) =>
            (ui, val) =>
            {
                changeFunc(ui, val);
                return GetUserStateDescription(ui);
            };
        
        private string GetUserStateDescription(BotUserInfo userInfo)
        {
            return "Your registration info was updated:" + Environment.NewLine
                 + "Phone: " + userInfo.Data.Phone + Environment.NewLine
                 + "Email: " + userInfo.Data.Email + Environment.NewLine
                 + "Name: " + userInfo.Data.Name + Environment.NewLine
                 + "Landlord message: " + userInfo.Data.EmailTemplate + Environment.NewLine + Environment.NewLine
                 + $"Your filters: maxPrice={userInfo.Data.MaxPrice}, minRoomsNumber={userInfo.Data.MinRoomNumber}";
        }
    }
}