namespace Namaa.Domain.ReferenceData;


public sealed class Crop
{
<<<<<<< HEAD
    // --- Read-Only Properties ---
=======
>>>>>>> dev-alaa
    public int Id { get; } 
    public int GovernorateId { get; }
    public Governorate? Governorate { get; } 
    public string? ImageUrl { get; }
    public int DaysToHarvest { get; }
    
<<<<<<< HEAD
=======
    // 🌍 Standard English Names
>>>>>>> dev-alaa
    public string? Name { get; }
    public string? Category { get; } 
    
    public string? Season { get; }
    public string? FarmingMethod { get; }
    public string? PlantingTime { get; }
    public string? HarvestTime { get; }
    
    public int MinTemperature { get; } 
    public int MaxTemperature { get; } 
<<<<<<< HEAD
    public List<string> SupportedIrrigationMethods {get;} =new();
    public List<string> SupportedEnvironmentTypes {get;}=new ();
    public string? IrrigationLevel { get; }
    public string? WaterRequirementCategory { get; } 

    // --- 🛡️ Encapsulated Collection ---
    // The private list handles the data storage.
    private readonly List<SoilType> _suitableSoilTypes = new();

    // The public property provides a read-only view. 
    // This is a "Calculated Property" so it doesn't need a setter.
    public IReadOnlyCollection<SoilType> SuitableSoilTypes => _suitableSoilTypes.AsReadOnly();

    // --- Financials & Production ---
    public double MinProductionPerDonum { get; set;}
    public double MaxProductionPerDonum { get;set; }
    public decimal MinExpectedPrice { get;set; }
    public decimal MaxExpectedPrice { get;set; }
    public decimal MinEstimatedCost { get;set; }
    public decimal MaxEstimatedCost { get;set; }

    // Private constructor for Entity Framework Core
    private Crop() { }

    // Public Constructor
=======
    
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

>>>>>>> dev-alaa
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
<<<<<<< HEAD
        List<SoilType> suitableSoilTypes,
        List<string> supportedIrrigationMethods,
        List<string> supportedEnvironmentTypes,
=======
        List<string> suitableSoilTypes,
>>>>>>> dev-alaa
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
<<<<<<< HEAD
        
        _suitableSoilTypes=suitableSoilTypes;       
        SupportedEnvironmentTypes=supportedEnvironmentTypes;
        SupportedIrrigationMethods=supportedIrrigationMethods;

=======
        SuitableSoilTypes = suitableSoilTypes ?? new List<string>();
>>>>>>> dev-alaa
        MinProductionPerDonum = minProductionPerDonum;
        MaxProductionPerDonum = maxProductionPerDonum;
        MinExpectedPrice = minExpectedPrice;
        MaxExpectedPrice = maxExpectedPrice;
        MinEstimatedCost = minEstimatedCost;
        MaxEstimatedCost = maxEstimatedCost;
        ImageUrl = imageUrl;
        DaysToHarvest = daysToHarvest;
    }
<<<<<<< HEAD

=======
>>>>>>> dev-alaa
}