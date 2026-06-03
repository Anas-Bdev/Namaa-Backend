using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.CancelOrder;

public class CancelProductOrderCommandHandler(IAppDbContext context) : IRequestHandler<CancelProductOrderCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(CancelProductOrderCommand request, CancellationToken cancellationToken)
    {
         var order=await context.ProductOrders.FindAsync([request.OrderId],cancellationToken);
        if(order is null)
        return ApplicationErrors.OrderNotFound;
       var listing=await context.ProductListings.FirstOrDefaultAsync(x => x.Id==order.ProductListingId,cancellationToken);
        if(order.TraderId!=request.TraderId)
        return ApplicationErrors.Forbidden;
        var cancelResult=order.Cancel();
        var inventoryResult=listing?.IncreaseQuantity(order.Quantity);
        if(cancelResult.IsError)
        return cancelResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}