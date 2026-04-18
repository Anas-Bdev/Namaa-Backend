using MediatR;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    Guid TraderId,
    Guid ListingId,
    double Quantity
) : IRequest<Result<OrderDto>>;