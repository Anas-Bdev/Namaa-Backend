using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.ReferenceData;

namespace Namaa.Domain.Land;
public sealed class Land : AuditableEntity
{
    public Guid FarmerId { get; }
    public int GovernorateId { get; private set; }
    public int SoilTypeId { get; private set; }
    public string? Name { get; private set; }
    public double Area { get; private set; }
    public WaterSourceType WaterSourceType { get; private set; }
    public WaterAvailability WaterAvailability { get; private set; }
    public EnvironmentType EnvironmentType { get; private set; }
    
    public IrrigationMethod IrrigationMethod { get; private set; }
    public Governorate? Governorate { get; private set; }
    public SoilType? SoilType { get; private set; }

    private Land() { }

    private Land(
        Guid id, 
        Guid farmerId, 
        int cityId, 
        int soilId, 
        string name, 
        double area, 
        WaterSourceType waterSourceType, 
        WaterAvailability waterAvailability, 
        EnvironmentType environmentType,
        IrrigationMethod irrigationMethod) 
        : base(id)
    {
        FarmerId = farmerId;
        GovernorateId = cityId;
        SoilTypeId = soilId;
        Name = name;
        Area = area;
        WaterSourceType = waterSourceType;
        WaterAvailability = waterAvailability;
        EnvironmentType = environmentType;
        IrrigationMethod = irrigationMethod; 
    }

    public static Result<Land> Create(
        Guid id, 
        Guid farmerId, 
        int cityId, 
        int soilId, 
        string name, 
        double area, 
        WaterSourceType waterSourceType, 
        WaterAvailability waterAvailability, 
        EnvironmentType environmentType,
        IrrigationMethod irrigationMethod) 
    {
        if (id == Guid.Empty)
            return LandErrors.IdRequired;

        if (farmerId == Guid.Empty)
            return LandErrors.FarmerIdRequired;

        if (cityId <= 0)
            return LandErrors.CityRequired;

        if (soilId <= 0)
            return LandErrors.SoilRequired;

        if (string.IsNullOrWhiteSpace(name))
            return LandErrors.NameRequired;

        if (area <= 0)
            return LandErrors.AreaInvalid;

        return new Land(
            id, 
            farmerId, 
            cityId, 
            soilId, 
            name.Trim(), 
            area, 
            waterSourceType, 
            waterAvailability, 
            environmentType, 
            irrigationMethod); 
    }

    public Result<Updated> Update(
        int cityId, 
        int soilId, 
        string name, 
        double area, 
        WaterSourceType waterSourceType, 
        WaterAvailability waterAvailability, 
        EnvironmentType environmentType,
        IrrigationMethod irrigationMethod) 
    {
        if (cityId <= 0)
            return LandErrors.CityRequired;

        if (soilId <= 0)
            return LandErrors.SoilRequired;

        if (string.IsNullOrWhiteSpace(name))
            return LandErrors.NameRequired;

        if (area <= 0)
            return LandErrors.AreaInvalid;

        GovernorateId = cityId;
        SoilTypeId = soilId;
        Name = name.Trim(); 
        Area = area;
        WaterSourceType = waterSourceType;
        WaterAvailability = waterAvailability;
        EnvironmentType = environmentType;
        IrrigationMethod = irrigationMethod; 

        return Result.Updated;
    }
}