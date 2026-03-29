namespace Namaa.Domain.ReferenceData;


public sealed class Crop
{
    public int Id { get; } 
    public int GovernorateId { get; }
    public Governorate? Governorate { get; } 
    public string? ImageUrl { get; }
    public int DaysToHarvest { get; }
    
    // 🌍 Standard English Names
    public string? Name { get; }
    public string? Category { get; } 
    
    public string? Season { get; }
    public string? FarmingMethod { get; }
    public string? PlantingTime { get; }
    public string? HarvestTime { get; }
    
    public int MinTemperature { get; } 
    public int MaxTemperature { get; } 
    
    public string? IrrigationLevel { get; }
    public string? WaterRequirementCategory { get; } 
    public List<string> SuitableSoilTypes { get; } = new();

    // ⚖️ Production (in Tons)
    public double MinProductionPerDonum { get; }
    public double MaxProductionPerDonum { get; }

    // 💰 Financials (in ILS)
    public decimal MinExpectedPrice { get; }
    public decimal MaxExpectedPrice { get; }
    
    public decimal MinEstimatedCost { get; }
    public decimal MaxEstimatedCost { get; }

    private Crop() { }

    public Crop(
        int id,
        int governorateId, 
        string? name, 
        string? category, 
        string? season, 
        string? farmingMethod, 
        string? plantingTime, 
        string? harvestTime, 
        int minTemperature, 
        int maxTemperature, 
        string? irrigationLevel, 
        string? waterRequirementCategory, 
        List<string> suitableSoilTypes,
        double minProductionPerDonum, 
        double maxProductionPerDonum, 
        decimal minExpectedPrice, 
        decimal maxExpectedPrice, 
        decimal minEstimatedCost, 
        decimal maxEstimatedCost,
        string? imageUrl,
        int daysToHarvest)
    {
        Id = id;
        GovernorateId = governorateId; 
        Name = name;
        Category = category;
        Season = season;
        FarmingMethod = farmingMethod;
        PlantingTime = plantingTime;
        HarvestTime = harvestTime;
        MinTemperature = minTemperature;
        MaxTemperature = maxTemperature;
        IrrigationLevel = irrigationLevel;
        WaterRequirementCategory = waterRequirementCategory;
        SuitableSoilTypes = suitableSoilTypes ?? new List<string>();
        MinProductionPerDonum = minProductionPerDonum;
        MaxProductionPerDonum = maxProductionPerDonum;
        MinExpectedPrice = minExpectedPrice;
        MaxExpectedPrice = maxExpectedPrice;
        MinEstimatedCost = minEstimatedCost;
        MaxEstimatedCost = maxEstimatedCost;
        ImageUrl = imageUrl;
        DaysToHarvest = daysToHarvest;
    }
}