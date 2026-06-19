using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.ConfirmOrder;

public class ConfirmProductOrderCommandHandler(IAppDbContext context,INotificationService notificationService) : IRequestHandler<ConfirmProductOrderCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ConfirmProductOrderCommand request, CancellationToken cancellationToken)
    {
      var order=await context.ProductOrders.FindAsync([request.OrderId],cancellationToken);
        if(order is null)
        return ApplicationErrors.OrderNotFound;
        var confirmResult=order.Confirm();
        if(confirmResult.IsError)
        return confirmResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        await notificationService.SendNotificationAsync(
        userId: order.TraderId,
        title: "Order Update",
        message: $"Your order #{order.OrderNumber} has been {order.Status.ToString().ToLower()}.",
        type: "OrderConfirmed",
        referencedId: order.Id
        );

        return Result.Updated;
    }
}