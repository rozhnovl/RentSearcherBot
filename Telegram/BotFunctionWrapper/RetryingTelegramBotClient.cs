using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests.Abstractions;

namespace BotFunctionWrapper
{
    internal class RetryingTelegramBotClient : ITelegramBotClient
    {
        private ITelegramBotClient _telegramBotClientImplementation;
        private readonly AsyncPolicy _retryPolicy;

        public RetryingTelegramBotClient(ITelegramBotClient implementation, AsyncPolicy retryPolicy)
        {
            _telegramBotClientImplementation = implementation;
            _retryPolicy = retryPolicy;
        }

        public async Task<TResponse> MakeRequestAsync<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _retryPolicy.ExecuteAsync(() =>
                _telegramBotClientImplementation.MakeRequestAsync(request, cancellationToken));
        }

        public async Task<bool> TestApiAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await _retryPolicy.ExecuteAsync(() =>
                _telegramBotClientImplementation.TestApiAsync(cancellationToken));
        }

        public async Task DownloadFileAsync(string filePath, Stream destination,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await _retryPolicy.ExecuteAsync(() =>
                _telegramBotClientImplementation.DownloadFileAsync(filePath, destination, cancellationToken));
        }

        public long? BotId => _telegramBotClientImplementation.BotId;

        public TimeSpan Timeout
        {
            get => _telegramBotClientImplementation.Timeout;
            set => _telegramBotClientImplementation.Timeout = value;
        }

        public IExceptionParser ExceptionsParser
        {
            get => _telegramBotClientImplementation.ExceptionsParser;
            set => _telegramBotClientImplementation.ExceptionsParser = value;
        }

        public event AsyncEventHandler<ApiRequestEventArgs> OnMakingApiRequest
        {
            add => _telegramBotClientImplementation.OnMakingApiRequest += value;
            remove => _telegramBotClientImplementation.OnMakingApiRequest -= value;
        }

        public event AsyncEventHandler<ApiResponseEventArgs> OnApiResponseReceived
        {
            add => _telegramBotClientImplementation.OnApiResponseReceived += value;
            remove => _telegramBotClientImplementation.OnApiResponseReceived -= value;
        }
    }
}