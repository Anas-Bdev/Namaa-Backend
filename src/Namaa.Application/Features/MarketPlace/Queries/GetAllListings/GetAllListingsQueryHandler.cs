using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.MarketPlace;

namespace Namaa.Application.Features.MarketPlace.Queries.GetAllListings;

public class GetAllListingsQueryHandler(IAppDbContext context) : IRequestHandler<GetAllListingsQuery, Result<PaginatedList<ProductListingDto>>>
{
    public async Task<Result<PaginatedList<ProductListingDto>>> Handle(GetAllListingsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ProductListing> query= context.ProductListings.AsNoTracking()
                           .Include(x => x.Crop);
        if(!string.IsNullOrWhiteSpace(request.Category))
        query=query.Where(x => x.Crop!.Category==request.Category);
        
        if(!string.IsNullOrWhiteSpace(request.Location))
        query=query.Where(x => x.Crop!.Governorate!.Name==request.Location);

        if(request.MinPrice.HasValue)
        query=query.Where(x => x.PricePerUnit >= request.MinPrice.Value);

        if(request.MaxPrice.HasValue)
        query=query.Where(x => x.PricePerUnit <= request.MaxPrice.Value);

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var listings = await query
        .OrderByDescending(p => p.CreatedAtUtc)
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToListAsync(cancellationToken);

        var items=listings.ToDtos();

        return new PaginatedList<ProductListingDto>
        {
        PageNumber = request.PageNumber,
        PageSize = request.PageSize,
        TotalCount = totalCount,
        TotalPages = totalPages,
        Items = items
        };
    }
}