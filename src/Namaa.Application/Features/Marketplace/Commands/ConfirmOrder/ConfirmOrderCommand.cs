using MediatR;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Commands.ConfirmOrder;

public sealed record ConfirmOrderCommand(
    Guid OrderId,
    Guid FarmerId
) : IRequest<Result<OrderDto>>;