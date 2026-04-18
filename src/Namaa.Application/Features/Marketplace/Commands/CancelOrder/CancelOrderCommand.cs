using MediatR;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Commands.CancelOrder;

public sealed record CancelOrderCommand(
    Guid OrderId,
    Guid UserId
) : IRequest<Result<OrderDto>>;