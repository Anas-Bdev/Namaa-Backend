using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.Marketplace;

namespace Namaa.Application.Features.Marketplace.Commands.DeleteListing;

public class DeleteListingCommandHandler(IAppDbContext context)
    : IRequestHandler<DeleteListingCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(
        DeleteListingCommand request,
        CancellationToken cancellationToken)
    {
        var listing = await context.ProductListings
            .Include(l => l.Orders)
            .FirstOrDefaultAsync(l => l.Id == request.ListingId, cancellationToken);

        if (listing is null)
            return ApplicationErrors.ListingNotFound;

        var hasActiveOrders = listing.Orders
            .Any(o => o.Status != OrderStatus.Cancelled);

        if (hasActiveOrders)
            return ApplicationErrors.ListingHasActiveOrders;

        context.ProductListings.Remove(listing);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}