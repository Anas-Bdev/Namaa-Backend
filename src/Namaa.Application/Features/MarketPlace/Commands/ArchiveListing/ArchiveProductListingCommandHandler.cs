using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.ArchiveListing;

public class ArchiveProductListingCommandHandler(IAppDbContext context,HybridCache cache) : IRequestHandler<ArchiveProductListingCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ArchiveProductListingCommand request, CancellationToken cancellationToken)
    {
        var listing=await context.ProductListings.FindAsync([request.ListingId],cancellationToken);
        if(listing is null)
        return ApplicationErrors.ListingNotFound;
        if(listing.FarmerId!=request.FarmerId)
        return ApplicationErrors.Forbidden;
        var result=listing.Archive();
        if(result.IsError)
        return result.Errors;
        await context.SaveChangesAsync(cancellationToken);
        await cache.RemoveByTagAsync("listings",cancellationToken);
        return Result.Updated;
    }
}