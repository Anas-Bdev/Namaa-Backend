using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.ReferenceData;

namespace Namaa.Domain.Profiles.Trader;
public sealed class TraderProfile : AuditableEntity
{
    // Properties
    public string? BusinessName { get; private set; }
    public TraderType BusinessType { get; private set; } 
    
    // Location Data
    public int GovernorateId { get; private set; }
    public Governorate? Governorate { get; private set; }
    public string? AddressDetail { get; private set; }

    // Private constructor for EF Core
    private TraderProfile() { }

    // Private parameterized constructor
    private TraderProfile(
        Guid id, 
        string businessName, 
        TraderType businessType,
        int governorateId, 
        string? addressDetail) : base(id)
    {
        BusinessName = businessName;
        BusinessType = businessType;
        GovernorateId = governorateId;
        AddressDetail = addressDetail;
    }

    /// <summary>
    /// Factory method to create a valid TraderProfile.
    /// </summary>
    public static Result<TraderProfile> Create(
        Guid id, 
        string businessName, 
        TraderType businessType,
        int governorateId, 
        string? addressDetail)
    {
        if (id == Guid.Empty) 
            return TraderErrors.UserIdRequired;

        if (governorateId <= 0) 
            return TraderErrors.CityRequired;

        if (string.IsNullOrWhiteSpace(businessName)) 
            return TraderErrors.BusinessNameRequired;

        if (!Enum.IsDefined(typeof(TraderType), businessType)) 
            return TraderErrors.InvalidTraderType;

        return new TraderProfile(
            id, 
            businessName, 
            businessType, 
            governorateId, 
            addressDetail);
    }


    public Result<Updated> UpdateProfile(
        string businessName,
        TraderType businessType,
        int governorateId,
        string? addressDetail)
    {
        if (governorateId <= 0) 
            return TraderErrors.CityRequired;

        if (string.IsNullOrWhiteSpace(businessName)) 
            return TraderErrors.BusinessNameRequired;

        if (!Enum.IsDefined(typeof(TraderType), businessType)) 
            return TraderErrors.InvalidTraderType;

        BusinessName = businessName;
        BusinessType = businessType;
        GovernorateId = governorateId;
        AddressDetail = addressDetail;

        return Result.Updated;
    }
}