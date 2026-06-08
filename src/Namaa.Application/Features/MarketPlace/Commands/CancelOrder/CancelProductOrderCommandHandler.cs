using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.CancelOrder;

public class CancelProductOrderCommandHandler(IAppDbContext context,HybridCache cache) : IRequestHandler<CancelProductOrderCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(CancelProductOrderCommand request, CancellationToken cancellationToken)
    {
        var order=await context.ProductOrders.FindAsync([request.OrderId],cancellationToken);
        if(order is null)
        return ApplicationErrors.OrderNotFound;
        if(order.TraderId!=request.TraderId)
        return ApplicationErrors.Forbidden;
        var listing=await context.ProductListings.FindAsync([order.ProductListingId],cancellationToken);
        var cancelResult=order.Cancel();
        if(cancelResult.IsError)
        return cancelResult.Errors;
        // ProductOrder has a required FK to ProductListing.
        var inventoryResult=listing!.IncreaseQuantity(order.Quantity);
        if(inventoryResult.IsError)
        return inventoryResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        await cache.RemoveByTagAsync("listings",cancellationToken);
        return Result.Updated;
    }
}