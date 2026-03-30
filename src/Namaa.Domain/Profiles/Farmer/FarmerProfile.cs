using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Farmer;

public sealed class FarmerProfile : AuditableEntity
{
    public string? Description { get; private set; }
    public int? CityId { get; private set; }
    public string? AddressDetail { get; private set; }
    public string? ExperienceLevel { get; private set; }

    private FarmerProfile() { }

    private FarmerProfile(Guid id) : base(id) { }

    public static Result<FarmerProfile> Create(Guid id)
    {
        if (id == Guid.Empty)
            return FarmerErrors.UserIdRequired;
        return new FarmerProfile(id);
    }

    public Result<Updated> UpdateProfile(
        string? description,
        int cityId,
        string? addressDetail,
        string? experienceLevel)
    {
        if (cityId <= 0)
            return FarmerErrors.CityRequired;
        Description = description;
        CityId = cityId;
        AddressDetail = addressDetail;
        ExperienceLevel = experienceLevel;
        return Result.Updated;
    }
}