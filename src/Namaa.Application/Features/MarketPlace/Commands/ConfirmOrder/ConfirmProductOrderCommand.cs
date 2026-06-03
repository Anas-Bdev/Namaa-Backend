using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.ConfirmOrder;
public sealed record ConfirmProductOrderCommand(
    Guid OrderId
):IRequest<Result<Updated>>;