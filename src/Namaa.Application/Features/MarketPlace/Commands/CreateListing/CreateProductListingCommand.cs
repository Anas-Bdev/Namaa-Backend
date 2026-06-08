using MediatR;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.CreateListing;
public sealed record CreateProductListingCommand(
    Guid FarmerId,
    Guid? SeedingCycleId,
    int CropId,
    string Title,
    string? Description,
    string Unit,
    decimal PricePerUnit,
    decimal? DiscountPrice,
    decimal QuantityAvailable,
    string? ImageUrl,
    DateTime? HarvestDate
):IRequest<Result<ProductListingDto>>;