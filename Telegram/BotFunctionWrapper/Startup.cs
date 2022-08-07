using System;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BotFunctionWrapper;

public abstract class Startup : FunctionsStartup
{
    private readonly string storageConnectionString;

    /// <summary>
    /// Default constructor, taking connection strings from environment variable "AzureStorageConnectionString"
    /// </summary>
    protected Startup() : this(Environment.GetEnvironmentVariable("AzureStorageConnectionString"))
    {
    }

    /// <summary>
    /// Default constructor, taking connection strings from environment variable "AzureStorageConnectionString"
    /// </summary>
    protected Startup(string storageConnectionString)
    {
        this.storageConnectionString = storageConnectionString;
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton(s =>
            new TableServiceClient(storageConnectionString));
        builder.Services.AddSingleton(s =>
            new QueueServiceClient(storageConnectionString));
        builder.Services.AddSingleton(s =>
            new BlobServiceClient(storageConnectionString));
        builder.Services.AddLogging();
    }
}