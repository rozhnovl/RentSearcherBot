using System;
using System.Text.Json;
using Azure;
using Azure.Data.Tables;
using Contracts;

namespace RentSearcher.Crawler;

public class EstateListingEntity : ITableEntity
{
    public EstateListingEntity() { }
    public EstateListingEntity(EstateListing listing)
    {
        ValueJson = JsonSerializer.Serialize(listing);
        RowKey = "Sreality_" + listing.hash_id;
        PartitionKey = listing.hash_id.ToString();
    }
    public string ValueJson { get; set; }

    public EstateListing GetValue() => JsonSerializer.Deserialize<EstateListing>(ValueJson);
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}