using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.ShipOrder;
public sealed record ShipProductOrderCommand(
    Guid OrderId,
    DateTime EstimatedArrivalDate

):IRequest<Result<Updated>>;