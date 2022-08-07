using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using Azure.Data.Tables;
using Microsoft.Extensions.DependencyInjection;
using Azure.Storage.Queues;

[assembly: FunctionsStartup(typeof(RentSearcher.Crawler.Startup))]
namespace RentSearcher.Crawler
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(s =>
                new TableServiceClient(Environment.GetEnvironmentVariable("AzureStorageConnectionString")));
            builder.Services.AddSingleton(s =>
                new QueueServiceClient(Environment.GetEnvironmentVariable("AzureStorageConnectionString")));
            builder.Services.AddLogging();
        }
    }
}