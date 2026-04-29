namespace Namaa.Api.Contracts.Requests.Marketplace;

public record CreateListingRequest(
    Guid SeedingCycleId,
    string Title,
    string? Description,
    string? Unit,
    decimal PricePerUnit,
    decimal? DiscountPrice,
    double QuantityAvailable,
    string? ImageUrl,
    DateTime? HarvestDate
);