using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Application.Features.Marketplace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Marketplace;

namespace Namaa.Application.Features.Marketplace.Commands.UpdateListing;

public class UpdateListingCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<UpdateListingCommand, Result<ProductListingDto>>
{
    public async Task<Result<ProductListingDto>> Handle(
        UpdateListingCommand request,
        CancellationToken cancellationToken)
    {
        var listing = await context.ProductListings
            .FirstOrDefaultAsync(l => l.Id == request.ListingId, cancellationToken);

        if (listing is null)
            return ApplicationErrors.ListingNotFound;

        var seedingCycle = await context.SeedingCycles
            .FirstOrDefaultAsync(s => s.Id == listing.SeedingCycleId, cancellationToken);

        if (seedingCycle is null || seedingCycle.LandId != await GetLandIdByFarmer(request.FarmerId, cancellationToken))
            return ApplicationErrors.Forbidden;

        var result = listing.Update(
            request.Title,
            request.Description,
            request.Unit,
            request.PricePerUnit,
            request.DiscountPrice,
            request.QuantityAvailable,
            request.ImageUrl,
            request.HarvestDate
        );

        if (result.IsError)
            return result.Errors;

        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var farmerName = users.FirstOrDefault(u => u.Id == request.FarmerId)?.FullName ?? string.Empty;

        return listing.ToDto(farmerName);
    }

    private async Task<Guid> GetLandIdByFarmer(Guid farmerId, CancellationToken cancellationToken)
    {
        var land = await context.Lands
            .FirstOrDefaultAsync(l => l.FarmerId == farmerId, cancellationToken);
        return land?.Id ?? Guid.Empty;
    }
}