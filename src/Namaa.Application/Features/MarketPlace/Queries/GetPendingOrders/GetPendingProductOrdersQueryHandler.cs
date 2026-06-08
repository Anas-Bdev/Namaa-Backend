using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.MarketPlace.Queries.GetPendingOrders;

public class GetPendingProductOrdersQueryHandler(IAppDbContext context) : IRequestHandler<GetPendingProductOrdersQuery, Result<List<ProductOrderDto>>>
{
    public async Task<Result<List<ProductOrderDto>>> Handle(GetPendingProductOrdersQuery request, CancellationToken cancellationToken)
    {
        var pendingOrders = await context.ProductOrders
            .AsNoTracking()
            .Where(x => x.ProductListing!.FarmerId == request.FarmerId 
                     && x.Status == OrderStatus.Pending)
            .OrderByDescending(o => o.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return pendingOrders.ToDtos();
    }
}