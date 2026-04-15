namespace Namaa.Api.Contracts.Requests.Farmer;

public record UpdateFarmerProfileRequest(
    string? Description,
    int CityId,
    string? AddressDetail,
    string? ExperienceLevel
);
