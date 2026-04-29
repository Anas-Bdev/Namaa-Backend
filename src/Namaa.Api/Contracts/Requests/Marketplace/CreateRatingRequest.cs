namespace Namaa.Api.Contracts.Requests.Marketplace;

public record CreateRatingRequest(
    Guid OrderId,
    int RatingValue,
    string? Comment
);