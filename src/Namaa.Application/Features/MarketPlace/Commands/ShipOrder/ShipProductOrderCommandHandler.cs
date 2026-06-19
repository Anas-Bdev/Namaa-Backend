using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.ShipOrder;

public class ShipProductOrderCommandHandler(IAppDbContext context,INotificationService notificationService) : IRequestHandler<ShipProductOrderCommand, Result<Updated>>
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
     await notificationService.SendNotificationAsync(
     userId: order.TraderId,
     title: "Order Shipped!",
     message: $"Good news! Your order #{order.OrderNumber} has been shipped and is on its way.",
     type: "OrderShipped",
     referencedId: order.Id
     );
     return Result.Updated;

    }
}