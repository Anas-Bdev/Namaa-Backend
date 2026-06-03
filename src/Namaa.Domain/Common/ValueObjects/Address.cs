namespace Namaa.Domain.Common.ValueObjects;
public record Address
{
    public string Governorate {get;init;}
    public string CityOrVillage {get;init;}
    public string NeighborhoodOrStreet {get;init;}
    public string? LandMark {get;init;}
    #pragma warning disable CS8618
    private Address() {}
    #pragma warning restore CS8618
    public Address(string governorate, string cityOrVillage, string neighborhoodOrStreet, string? landMark = null)
    {
        Governorate=governorate;
        CityOrVillage=cityOrVillage;
        NeighborhoodOrStreet=neighborhoodOrStreet;
        LandMark=landMark;

    }
}