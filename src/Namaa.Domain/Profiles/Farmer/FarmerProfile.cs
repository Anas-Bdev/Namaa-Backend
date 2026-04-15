using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.ReferenceData;

namespace  Namaa.Domain.Profiles.Farmer;
public sealed class FarmerProfile : AuditableEntity
{
    // Properties
    public string? Description { get; private set; }
    public int GovernorateId { get; private set; }
    public Governorate? Governorate { get; private set; }
    public string? AddressDetail { get; private set; }

    // Private parameterless constructor for EF Core
    private FarmerProfile() { }

    // Private parameterized constructor for the Factory Method
    private FarmerProfile(
        Guid id, 
        string? description, 
        int governorateId, 
        string? addressDetail) : base(id)
    {
        Description = description;
        GovernorateId = governorateId;
        AddressDetail = addressDetail;
    }

    public static Result<FarmerProfile> Create(
        Guid id, 
        string? description, 
        int governorateId, 
        string? addressDetail)
    {
        if (id == Guid.Empty)
            return FarmerErrors.UserIdRequired;

        if (governorateId <= 0)
            return FarmerErrors.CityRequired;

        return new FarmerProfile(id, description, governorateId, addressDetail);
    }

    public Result<Updated> UpdateProfile(
        int governorateId,
        string? description,
        string? addressDetail)
    {
        if (governorateId <= 0) 
            return FarmerErrors.CityRequired;

        GovernorateId = governorateId;
        Description = description;
        AddressDetail = addressDetail;

        return Result.Updated; 
    }
}