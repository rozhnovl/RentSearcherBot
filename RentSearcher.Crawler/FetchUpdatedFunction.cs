using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Azure.Storage.Queues;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RentSearcher.Crawler.Connectors;

namespace RentSearcher.Crawler
{
    public class FetchUpdatedFunction
    {
        private readonly TableServiceClient tableServiceClient;
        private readonly QueueServiceClient queueServiceClient;

        public FetchUpdatedFunction(TableServiceClient tableServiceClient, QueueServiceClient queueServiceClient)
        {
            this.tableServiceClient = tableServiceClient;
            this.queueServiceClient = queueServiceClient;
            tableServiceClient.GetTableClient("EstateListings").CreateIfNotExists();
        }                        

        [FunctionName("FetchUpdatedFunction")]
        public async Task Run([TimerTrigger("0 3/5 * * * * ")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            await ProcessUpdates(log);
        }

        [FunctionName("HttpTrigger")]
        public async Task RunHttp([HttpTrigger()] HttpRequest req, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            await ProcessUpdates(log);
        }

        private async Task ProcessUpdates(ILogger log)
        {
            try
            {
                var todayEntries = await new Sreality(log).FetchPageUrls();
                log.LogInformation($"A total of {todayEntries.Count} have been fetched.");
                foreach (var entry in todayEntries)
                {
                    if (!(await TableEntryExists(entry)))
                    {
                        var estateListing = new EstateListing(new Contracts.GpsLocation(entry.gps.lat, entry.gps.lon), entry.locality, entry.price, entry.name, entry.hash_id, await new Sreality(log).GetDetails(entry), entry.RoomNumber);
                        await tableServiceClient.GetTableClient("EstateListings").AddEntityAsync(new EstateListingEntity(estateListing));
                        await queueServiceClient.GetQueueClient("estatesaddedqueue").SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(estateListing))));
                    }
                }
            }
            catch(Exception e)
            {
                log.LogError(e, "Failed processing");
                throw;
            }
        }
        private async Task<bool> TableEntryExists(SRealityEstateListing entry)
        {
            await foreach (var _ in tableServiceClient.GetTableClient("EstateListings").QueryAsync<EstateListingEntity>(e => e.PartitionKey == entry.hash_id.ToString() && e.RowKey == "Sreality_" + entry.hash_id))
                return true;
            return false;
        }
    }
}
