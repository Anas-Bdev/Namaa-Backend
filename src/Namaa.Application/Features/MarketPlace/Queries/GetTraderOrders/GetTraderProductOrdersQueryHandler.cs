using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetTraderOrders;

public class GetTraderProductOrdersQueryHandler(IAppDbContext context) : IRequestHandler<GetTraderProductOrdersQuery, Result<List<ProductOrderDto>>>
{
    public async Task<Result<List<ProductOrderDto>>> Handle(GetTraderProductOrdersQuery request, CancellationToken cancellationToken)
    {
       var orders = await context.ProductOrders
            .AsNoTracking()
            .Where(o => o.TraderId == request.TraderId)
            .OrderByDescending(o => o.CreatedAtUtc)
            .ToListAsync(cancellationToken);
        
       return orders.ToDtos();
     
    }
}