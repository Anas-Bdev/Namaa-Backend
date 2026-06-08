using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.UpdateListing;
public sealed record UpdateProductListingCommand(
    Guid ListingId,
    Guid FarmerId,
    string Title,
    string? Description,
    int CropId,
    string Unit,
    decimal PricePerUnit,
    decimal? DiscountPrice,
    decimal QuantityAvailable,
    string? ImageUrl,
    DateTime? HarvestDate
):IRequest<Result<Updated>>;