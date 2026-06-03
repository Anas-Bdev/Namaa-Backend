using MediatR;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.CreateOrder;
public sealed record CreateProductOrderCommand(
    Guid TraderId,
    Guid ProductListingId,
    decimal Quantity,
    string Governorate,
    string CityOrVillage,
    string NeighborhoodOrStreet,
    string? LandMark,
    string? DeliveryNotes
):IRequest<Result<ProductOrderDto>>;