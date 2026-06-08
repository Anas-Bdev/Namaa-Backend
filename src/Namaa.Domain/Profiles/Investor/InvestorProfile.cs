using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.ReferenceData;

namespace Namaa.Domain.Profiles.Investor;
public sealed class InvestorProfile:AuditableEntity
{
    public InvestorType Type { get; private set; }
    public string? OrganizationName { get; private set; } 
    public int GovernorateId { get; private set; }
    public Governorate? Governorate { get; private set; }
    public string? AddressDetail { get; private set; }

    private InvestorProfile() { }

    private InvestorProfile(
        Guid id, 
        InvestorType type,
        string? organizationName, 
        int governorateId, 
        string? addressDetail) : base(id)
    {
        Type = type;
        OrganizationName = organizationName;
        GovernorateId = governorateId;
        AddressDetail = addressDetail;
    }

    public static Result<InvestorProfile> Create(
        Guid id, 
        InvestorType type,
        string? organizationName, 
        int governorateId, 
        string? addressDetail)
    {
        // 1. Standard Validation
        if (id == Guid.Empty) return InvestorErrors.UserIdRequired;
        if (governorateId <= 0) return InvestorErrors.CityRequired;
        if (!Enum.IsDefined(typeof(InvestorType), type)) return InvestorErrors.InvalidInvestorType;

        if (type != InvestorType.Individual && string.IsNullOrWhiteSpace(organizationName))
        {
            return InvestorErrors.OrganizationNameRequired;
        }

        return new InvestorProfile(id, type, organizationName, governorateId, addressDetail);
    }

    public Result<Updated> UpdateProfile(
        InvestorType type,
        string? organizationName,
        int governorateId,
        string? addressDetail)
    {
        if (governorateId <= 0) return InvestorErrors.CityRequired;
        if (!Enum.IsDefined(typeof(InvestorType), type)) return InvestorErrors.InvalidInvestorType;

        if (type != InvestorType.Individual && string.IsNullOrWhiteSpace(organizationName))
        {
            return InvestorErrors.OrganizationNameRequired;
        }

        Type = type;
        OrganizationName = organizationName;
        GovernorateId = governorateId;
        AddressDetail = addressDetail;

        return Result.Updated;
    }
}