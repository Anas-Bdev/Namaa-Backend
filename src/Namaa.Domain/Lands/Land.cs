using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.ReferenceData;

namespace Namaa.Domain.Lands;
public sealed class Land : AuditableEntity
{
    public Guid FarmerId { get; }
    public int GovernorateId { get; private set; }
    public double Latitude {get;private set;}
    public double Longitude {get;private set;}
    public string? AddressDetail {get;private set;}
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
        IrrigationMethod irrigationMethod,
        double latitude,
        double longitude,
        string addressDetail) 
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
        Latitude=latitude;
        Longitude=longitude;
        AddressDetail=addressDetail;
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
        IrrigationMethod irrigationMethod,
        double latitude,
        double longitude,
        string addressDetail) 
    {
        if (id == Guid.Empty)
            return LandErrors.IdRequired;

        if(string.IsNullOrWhiteSpace(addressDetail))    
        return LandErrors.AddressRequired;

        if (latitude < 31.0 || latitude > 33.0)
        return LandErrors.InvalidLatitude;

    // 3. Validate Longitude
       if (longitude < 34.0 || longitude > 36.0)
        return LandErrors.InvalidLongitude;

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
            irrigationMethod,
            latitude,
            longitude,
            addressDetail); 
    }

    public Result<Updated> Update(
        int cityId, 
        int soilId, 
        string name, 
        double area, 
        WaterSourceType waterSourceType, 
        WaterAvailability waterAvailability, 
        EnvironmentType environmentType,
        IrrigationMethod irrigationMethod,
        double latitude,
        double longitude,
        string addressDetail) 
    {
        
        if(string.IsNullOrWhiteSpace(addressDetail))    
        return LandErrors.AddressRequired;
        
         if (latitude < 31.0 || latitude > 33.0)
        return LandErrors.InvalidLatitude;

    // 3. Validate Longitude
       if (longitude < 34.0 || longitude > 36.0)
        return LandErrors.InvalidLongitude;

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
        Longitude=longitude;
        Latitude=latitude;
        AddressDetail=addressDetail;

        return Result.Updated;
    }
}