using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.DeliverOrder;

public class DeliverProductOrderCommandHandler(IAppDbContext context, INotificationService notificationService) 
    : IRequestHandler<DeliverProductOrderCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(DeliverProductOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await context.ProductOrders
            .Include(o => o.ProductListing)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

        if (order is null)
            return ApplicationErrors.OrderNotFound;

        
        if (order.ProductListing!.FarmerId != request.FarmerId)
            return ApplicationErrors.Forbidden;

        var deliverResult = order.Deliver();
        if (deliverResult.IsError)
            return deliverResult.Errors;

        await context.SaveChangesAsync(cancellationToken);

        await notificationService.SendNotificationAsync(
            userId: order.TraderId,
            title: "Delivery Confirmed",
            message: $"The farmer has confirmed receipt of order #{order.OrderNumber}.",
            type: "OrderDelivered",
            referencedId: order.Id
        );

        return Result.Updated;
    }
}