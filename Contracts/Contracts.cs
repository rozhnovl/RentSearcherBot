namespace Contracts;

public record EstateDetails(string Description, string[] images, bool? hasElevator, string? floor);
public record EstateListing(GpsLocation gps, string locality, int price, string name, long hash_id, EstateDetails Details, int RoomNumber)
{
    public string DetailsUrl => $"https://www.sreality.cz/detail/pronajem/byt/1+kk/{locality}/{hash_id}";
}
public record GpsLocation(decimal lat, decimal lon);