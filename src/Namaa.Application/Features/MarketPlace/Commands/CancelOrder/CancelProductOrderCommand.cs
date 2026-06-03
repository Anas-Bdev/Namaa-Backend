using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.CancelOrder;
public sealed record CancelProductOrderCommand(
    Guid OrderId,
    Guid TraderId
):IRequest<Result<Updated>>;