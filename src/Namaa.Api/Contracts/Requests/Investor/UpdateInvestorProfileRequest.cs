namespace Namaa.Api.Contracts.Requests.Investor;

public record UpdateInvestorProfileRequest(
    string OrganizationName,
    string? CompanyName,
    int CityId,
    string? AddressDetail
);