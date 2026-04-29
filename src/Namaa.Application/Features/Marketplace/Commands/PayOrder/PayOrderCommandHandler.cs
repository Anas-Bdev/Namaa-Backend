using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Application.Features.Marketplace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Marketplace;

namespace Namaa.Application.Features.Marketplace.Commands.PayOrder;

public class PayOrderCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<PayOrderCommand, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(
        PayOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .Include(o => o.Listing)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

        if (order is null)
            return ApplicationErrors.OrderNotFound;

        if (order.TraderId != request.TraderId)
            return ApplicationErrors.Forbidden;

        var result = order.Pay();
        if (result.IsError)
            return result.Errors;

        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var traderName = users.FirstOrDefault(u => u.Id == order.TraderId)?.FullName ?? string.Empty;

        return order.ToDto(traderName, order.Listing!.Title);
    }
}