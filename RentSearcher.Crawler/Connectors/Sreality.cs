using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Contracts;
using Microsoft.Extensions.Logging;

namespace RentSearcher.Crawler.Connectors
{
    public class Sreality
    {
        private readonly ILogger _log;
        private const string FetchFlatsUrl = "https://www.sreality.cz/api/cs/v2/estates?category_main_cb=1&category_type_cb=2&estate_age=2&per_page=1000&locality_region_id=11%7C10";
        private const string FetchDetailsUrl = "https://www.sreality.cz/api/cs/v2/estates/{0}";

        public Sreality(ILogger log)
        {
            _log = log;
        }
        public async Task<ICollection<SRealityEstateListing>> FetchPageUrls()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(FetchFlatsUrl);
            var responseString = await response.Content.ReadAsStringAsync();
            _log.LogInformation("FetchPageUrls got response from Sreality: " + responseString);
            response.EnsureSuccessStatusCode();
            var responseObject = JsonSerializer.Deserialize<SRealityListResponse>(responseString);
            return responseObject._embedded.estates;
        }
        public async Task<EstateDetails> GetDetails(SRealityEstateListing listing)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(string.Format(FetchDetailsUrl, listing.hash_id));
            var responseString = await response.Content.ReadAsStringAsync();
            _log.LogInformation("GetDetails got response from Sreality: "+ responseString);
            response.EnsureSuccessStatusCode();
            var responseObject = JsonSerializer.Deserialize<SRealityEstateDetails>(responseString);
            return new EstateDetails(responseObject.text.value.GetString(), responseObject._embedded.images.Select(i => i._links.self.href).ToArray(),
                responseObject.HasElevator,
                responseObject.Floor);
        }
    }
    internal class SRealityListResponse
    {
        public SRealityEmbededResponse _embedded { get; set; }
    }
    internal class SRealityEmbededResponse
    {
        public List<SRealityEstateListing> estates { get; set; }
    }

    public record SRealityEstateDetails(SRealityProperty text, SRealityEstateDetailsEmbeded _embedded,
        SRealityProperty[] items)
    {
        public bool? HasElevator => items.FirstOrDefault(i => i.name == "Výtah")?.TryGetValue()?.Equals("true");

        public string? Floor
        {
            get
            {
                var floorLine = items.FirstOrDefault(i => i.name == "Podlaží")?.TryGetValue();
                floorLine = floorLine.Replace("podlaží", "").Replace(".", "").Trim();
                return floorLine;
            }
        }
    };
    public record SRealityEstateDetailsEmbeded(SRealityImageInfo[] images);
    public record SRealityImageInfo(SRealityImageLinks _links);
    public record SRealityImageLinks(SRealityImageRef self);
    public record SRealityImageRef(string href, string title);

    public record SRealityProperty(string name, JsonElement value, string type, string unit)
    {
        public string TryGetValue() =>
            type != "set" ? (type == "string" ? value.GetString() : value.GetRawText()) : null;
    }

    public record SRealityEstateListing(GpsLocation gps, string locality, int price, string name, long hash_id)
    {
        public string DetailsUrl { get { return $"https://www.sreality.cz/detail/pronajem/byt/1+kk/{locality}/{hash_id}"; } }
        public int RoomNumber
        {
            get
            {
                if (name.Contains("1kk") || name.Contains("1+kk") || name.Contains("pokoje"))
                    return 1;
                if (name.Contains("2kk") || name.Contains("2+kk") || name.Contains("1+1") || name.Contains("2 pokojů"))
                    return 2;
                if (name.Contains("3kk") || name.Contains("3+kk") || name.Contains("2+1") || name.Contains("3 pokojů"))
                    return 3;
                if (name.Contains("4kk") || name.Contains("4+kk") || name.Contains("3+1") || name.Contains("4 pokojů"))
                    return 4;
                if (name.Contains("5kk") || name.Contains("5+kk") || name.Contains("4+1") || name.Contains("5 pokojů"))
                    return 5;
                if (name.Contains("6kk") || name.Contains("6+kk") || name.Contains("5+1") || name.Contains("6 pokojů"))
                    return 6;
                if (name.Contains("7kk") || name.Contains("7+kk") || name.Contains("6+1") || name.Contains("7 pokojů"))
                    return 7;
                if (name.Contains("atypické"))
                    return -1;

                throw new ArgumentException($"Failed to parse rooms number, name is '{name}'.");
            }
        }
    }
    public record GpsLocation(decimal lat, decimal lon);
}