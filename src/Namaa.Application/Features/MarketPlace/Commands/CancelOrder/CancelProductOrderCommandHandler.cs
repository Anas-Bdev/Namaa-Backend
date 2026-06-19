using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.CancelOrder;

public class CancelProductOrderCommandHandler(IAppDbContext context,HybridCache cache,INotificationService notificationService) : IRequestHandler<CancelProductOrderCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(CancelProductOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await context.ProductOrders
            .Include(o => o.ProductListing) 
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

        if (order is null)
            return ApplicationErrors.OrderNotFound;
            
        if (order.TraderId != request.TraderId)
            return ApplicationErrors.Forbidden;

        var listing = order.ProductListing!;

        var cancelResult = order.Cancel();
        if (cancelResult.IsError)
            return cancelResult.Errors;
            
        var inventoryResult = listing.IncreaseQuantity(order.Quantity);
        if (inventoryResult.IsError)
            return inventoryResult.Errors;

        await context.SaveChangesAsync(cancellationToken);
        
        await notificationService.SendNotificationAsync(
            userId: listing.FarmerId, 
            title: "Order Cancelled",
            message: $"Order #{order.OrderNumber} has been cancelled.",
            type: "OrderCancelled",
            referencedId: order.Id
        );
        
        await cache.RemoveByTagAsync("listings", cancellationToken);
        
        return Result.Updated;
    }
}