namespace Contracts
{
    public record GpsLocation(decimal lat, decimal lon);

    public record EstateListing(GpsLocation gps, string locality, int price, string name, long hash_id, EstateDetails Details, int RoomNumber)
    {
        public string DetailsUrl { get { return $"https://www.sreality.cz/detail/pronajem/byt/1+kk/{locality}/{hash_id}"; } }
    }
    public record EstateDetails(string Description, string[] images, bool? hasElevator, string? floor);
}