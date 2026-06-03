using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.PayOrder;
public sealed record PayProductOrderCommand(
    Guid OrderId,
    Guid TraderId
):IRequest<Result<Updated>>;