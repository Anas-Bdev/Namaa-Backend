using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.DeliverOrder;

public class DeliverProductOrderCommandHandler(IAppDbContext context) : IRequestHandler<DeliverProductOrderCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(DeliverProductOrderCommand request, CancellationToken cancellationToken)
    {
         var order=await context.ProductOrders.FindAsync([request.OrderId],cancellationToken);
        if(order is null)
        return ApplicationErrors.OrderNotFound;
        if(order.TraderId!=request.TraderId)
        return ApplicationErrors.Forbidden;
        var deliverResult=order.Deliver();
        if(deliverResult.IsError)
        return deliverResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}