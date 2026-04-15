namespace Namaa.Api.Contracts.Requests.Trader;

public record UpdateTraderProfileRequest(
    string BusinessName,
    string BusinessType,
    string? PreferredCategories,
    int CityId,
    string? AddressDetail
);