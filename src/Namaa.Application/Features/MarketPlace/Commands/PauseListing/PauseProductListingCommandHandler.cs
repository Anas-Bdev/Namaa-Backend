using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.PauseListing;

public class PauseProductListingCommandHandler(IAppDbContext context,HybridCache cache) : IRequestHandler<PauseProductListingCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(PauseProductListingCommand request, CancellationToken cancellationToken)
    {
        var listing=await context.ProductListings.FindAsync([request.ListingId],cancellationToken);
        if(listing is null)
        return ApplicationErrors.ListingNotFound;
        if(listing.FarmerId!=request.FarmerId)
        return ApplicationErrors.Forbidden;
        var result=listing.Pause();
        if(result.IsError)
        return result.Errors;
        await context.SaveChangesAsync(cancellationToken);
        await cache.RemoveByTagAsync("listings",cancellationToken);
        return Result.Updated;
    }
}