using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetFarmerListings;

public class GetFarmerProductListingsQueryHandler(IAppDbContext context) : IRequestHandler<GetFarmerProductListingsQuery, Result<List<ProductListingDto>>>
{
    public async Task<Result<List<ProductListingDto>>> Handle(GetFarmerProductListingsQuery request, CancellationToken cancellationToken)
    {
        var listings = await context.ProductListings
            .AsNoTracking()
            .Where(x => x.FarmerId == request.FarmerId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
        
        return listings.ToDtos();
    }
}