using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.MarketPlace;

namespace Namaa.Application.Features.MarketPlace.Queries.GetListingById;

public class GetProductListingByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductListingByIdQuery, Result<ProductListingDto>>
{
    public async Task<Result<ProductListingDto>> Handle(GetProductListingByIdQuery request, CancellationToken cancellationToken)
    {
       var listing = await context.ProductListings
            .Include(x => x.Crop)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

      if(listing is null)
      return ApplicationErrors.ListingNotFound;
      return listing.ToDto();

      
            
    }
}