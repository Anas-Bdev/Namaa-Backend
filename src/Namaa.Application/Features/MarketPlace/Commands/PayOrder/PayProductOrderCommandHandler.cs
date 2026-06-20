using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.PayOrder;

public class PayProductOrderCommandHandler(IAppDbContext context, INotificationService notificationService) : IRequestHandler<PayProductOrderCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(PayProductOrderCommand request, CancellationToken cancellationToken)
    {
        var order=await context.ProductOrders.
                 Include(o => o.ProductListing)
                .FirstOrDefaultAsync(o => o.Id==request.OrderId);

        if(order is null)
        return ApplicationErrors.OrderNotFound;
        if(order.TraderId!=request.TraderId)
        return ApplicationErrors.Forbidden;
        var payResult=order.Pay();
        if(payResult.IsError)
        return payResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        await notificationService.SendNotificationAsync(
        userId: order.ProductListing!.FarmerId, 
        title: "Payment Confirmed",
        message: $"Payment has been successfully processed for order #{order.OrderNumber}.",
        type: "OrderPaid",
        referencedId: order.Id
        );
        return Result.Updated;
    }
}