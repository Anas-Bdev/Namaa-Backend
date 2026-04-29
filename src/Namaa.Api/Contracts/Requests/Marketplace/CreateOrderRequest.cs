namespace Namaa.Api.Contracts.Requests.Marketplace;

public record CreateOrderRequest(
    Guid ListingId,
    double Quantity
);