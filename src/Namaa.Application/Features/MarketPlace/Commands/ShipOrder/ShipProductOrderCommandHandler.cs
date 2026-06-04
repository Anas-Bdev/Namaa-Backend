using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.ShipOrder;

public class ShipProductOrderCommandHandler(IAppDbContext context) : IRequestHandler<ShipProductOrderCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ShipProductOrderCommand request, CancellationToken cancellationToken)
    {
     var order=await context.ProductOrders.FindAsync([request.OrderId],cancellationToken);
     if(order is null)
     return ApplicationErrors.OrderNotFound;
     var shipResult=order.Ship(request.EstimatedArrivalDate);
     if(shipResult.IsError)
     return shipResult.Errors;
     await context.SaveChangesAsync(cancellationToken);
     return Result.Updated;

    }
}