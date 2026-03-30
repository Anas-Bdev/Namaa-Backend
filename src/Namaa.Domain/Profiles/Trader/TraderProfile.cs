using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Trader;

public sealed class TraderProfile : AuditableEntity
{
    public string? BusinessName { get; private set; }
    public string? BusinessType { get; private set; }
    public string? PreferredCategories { get; private set; }
    public int? CityId { get; private set; }
    public string? AddressDetail { get; private set; }

    private TraderProfile() { }
    private TraderProfile(Guid id) : base(id) { }

    public static Result<TraderProfile> Create(Guid id)
    {
        if (id == Guid.Empty)
            return TraderErrors.UserIdRequired;
        return new TraderProfile(id);
    }

    public Result<Updated> UpdateProfile(
        string businessName,
        string businessType,
        string? preferredCategories,
        int cityId,
        string? addressDetail)
    {
        if (string.IsNullOrWhiteSpace(businessName))
            return TraderErrors.BusinessNameRequired;
        if (string.IsNullOrWhiteSpace(businessType))
            return TraderErrors.BusinessTypeRequired;
        if (cityId <= 0)
            return TraderErrors.CityRequired;

        BusinessName = businessName;
        BusinessType = businessType;
        PreferredCategories = preferredCategories;
        CityId = cityId;
        AddressDetail = addressDetail;

        return Result.Updated;
    }
}
