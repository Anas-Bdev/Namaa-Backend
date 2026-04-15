namespace Namaa.Domain.ReferenceData;
public sealed class Governorate
{
    public int Id {get;}
    public string? Name {get;}
    public float AvgSummerTemp {get;}
    public float AvgWinterTemp {get;}
    public int AvgRainfall {get;}
    public string? WaterAvailability { get; }
    private Governorate() {}
    public Governorate(int id, string name, float summerTemp, float winterTemp, int rainfall,string waterAvailability)
    {
        Id = id;
        Name = name;
        AvgSummerTemp = summerTemp;
        AvgWinterTemp = winterTemp;
        AvgRainfall = rainfall;
        WaterAvailability=waterAvailability;
    }
    
    
    
}