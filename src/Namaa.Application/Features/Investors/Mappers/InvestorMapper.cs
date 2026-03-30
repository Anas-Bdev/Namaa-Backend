using Namaa.Application.Features.Investors.Dtos;
using Namaa.Domain.Profiles.Investor;

namespace Namaa.Application.Features.Investors.Mappers;

public static class InvestorMapper
{
    public static InvestorProfileDto ToDto(this InvestorProfile investor, string fullName)
    {
        return new InvestorProfileDto
        {
            Id = investor.Id,
            FullName = fullName,
            OrganizationName = investor.OrganizationName,
            CompanyName = investor.CompanyName,
            CityId = investor.CityId ?? 0,
            AddressDetail = investor.AddressDetail
        };
    }

    public static InvestorSummaryDto ToSummaryDto(this InvestorProfile investor, string fullName)
    {
        return new InvestorSummaryDto
        {
            Id = investor.Id,
            FullName = fullName,
            OrganizationName = investor.OrganizationName,
            CompanyName = investor.CompanyName,
            CityId = investor.CityId ?? 0
        };
    }
}