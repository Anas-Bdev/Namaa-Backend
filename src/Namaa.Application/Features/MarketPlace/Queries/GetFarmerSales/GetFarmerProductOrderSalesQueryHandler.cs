using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetFarmerSales;

public class GetFarmerProductOrderSalesQueryHandler(IAppDbContext context) : IRequestHandler<GetFarmerProductOrderSalesQuery, Result<List<ProductOrderDto>>>
{
    public async Task<Result<List<ProductOrderDto>>> Handle(GetFarmerProductOrderSalesQuery request, CancellationToken cancellationToken)
    {
        var query = await context.ProductOrders
            .AsNoTracking()
            .Where(o => o.ProductListing.FarmerId == request.FarmerId)
            .ToListAsync(cancellationToken);

        return query.ToDtos();
    }
}