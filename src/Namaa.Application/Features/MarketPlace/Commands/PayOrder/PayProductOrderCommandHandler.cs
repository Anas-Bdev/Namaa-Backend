using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.PayOrder;

public class PayProductOrderCommandHandler(IAppDbContext context) : IRequestHandler<PayProductOrderCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(PayProductOrderCommand request, CancellationToken cancellationToken)
    {
        var order=await context.ProductOrders.FindAsync([request.OrderId],cancellationToken);
        if(order is null)
        return ApplicationErrors.OrderNotFound;
        if(order.TraderId!=request.TraderId)
        return ApplicationErrors.Forbidden;
        var payResult=order.Pay();
        if(payResult.IsError)
        return payResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}