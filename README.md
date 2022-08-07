# RentSearcher
The service consists of two apps:
* RentSearcher.Crawler - app checking boards (only SReality.cz for now) for new listings, parses it and passes new findings data to a queue
* TelegramMessageListener - implements Telegram interaction, sending updates to chats where the bot has been added, processes user interactions.
## Service configuration
Service is intended to be deployed to Azure cloud, utilizing Azure Functions as a serverless compute platform
Service dependences are:
* Azure Storage account, which needs to be configured as "AzureStorageConnectionString" configuration value for both functions.
* Telegram Bot configured https://core.telegram.org/bots#3-how-do-i-create-a-bot . BotToken should be passed to TelegramMessageListener configuration in "botToken" parameter
* Translator service (https://azure.microsoft.com/en-us/services/cognitive-services/translator/) is used to translate texts from Czech to Russian. It's connection key is configured as "TranslationServiceKey" property in Telegram function.
* Telegram interaction initialization requires to have AzF App Key as a configuration value as well, so it needs to be configured and placed to "apiKey" config value
* After functions deployment, Init HTTP endpoint should be called to configure Telegram integration properly.
