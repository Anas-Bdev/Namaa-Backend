using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Investor;

public sealed class InvestorProfile : AuditableEntity
{
    public string? OrganizationName { get; private set; }
    public string? CompanyName { get; private set; }
    public int? CityId { get; private set; }
    public string? AddressDetail { get; private set; }

    private InvestorProfile() { }
    private InvestorProfile(Guid id) : base(id) { }

    public static Result<InvestorProfile> Create(Guid id)
    {
        if (id == Guid.Empty)
            return InvestorErrors.UserIdRequired;
        return new InvestorProfile(id);
    }

    public Result<Updated> UpdateProfile(
        string organizationName,
        string? companyName,
        int cityId,
        string? addressDetail)
    {
        if (string.IsNullOrWhiteSpace(organizationName))
            return InvestorErrors.OrganizationNameRequired;
        if (cityId <= 0)
            return InvestorErrors.CityRequired;

        OrganizationName = organizationName;
        CompanyName = companyName;
        CityId = cityId;
        AddressDetail = addressDetail;

        return Result.Updated;
    }
}
