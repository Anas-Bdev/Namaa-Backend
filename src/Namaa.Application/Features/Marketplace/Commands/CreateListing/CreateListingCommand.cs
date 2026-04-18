using MediatR;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Commands.CreateListing;

public sealed record CreateListingCommand(
    Guid SeedingCycleId,
    Guid FarmerId,
    string Title,
    string? Description,
    string? Unit,
    decimal PricePerUnit,
    decimal? DiscountPrice,
    double QuantityAvailable,
    string? ImageUrl,
    DateTime? HarvestDate
) : IRequest<Result<ProductListingDto>>;