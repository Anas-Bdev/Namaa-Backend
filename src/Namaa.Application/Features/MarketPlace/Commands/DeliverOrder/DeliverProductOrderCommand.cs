using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.DeliverOrder;
public sealed record DeliverProductOrderCommand(
    Guid OrderId,
    Guid TraderId
):IRequest<Result<Updated>>;