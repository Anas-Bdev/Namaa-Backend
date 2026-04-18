using MediatR;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Commands.PayOrder;

public sealed record PayOrderCommand(
    Guid OrderId,
    Guid TraderId
) : IRequest<Result<OrderDto>>;