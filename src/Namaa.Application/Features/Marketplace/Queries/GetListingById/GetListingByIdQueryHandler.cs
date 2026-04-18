using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Application.Features.Marketplace.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Queries.GetListingById;

public class GetListingByIdQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetListingByIdQuery, Result<ProductListingDto>>
{
    public async Task<Result<ProductListingDto>> Handle(
        GetListingByIdQuery request,
        CancellationToken cancellationToken)
    {
        var listing = await context.ProductListings
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == request.ListingId, cancellationToken);

        if (listing is null)
            return ApplicationErrors.ListingNotFound;

        var seedingCycle = await context.SeedingCycles
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == listing.SeedingCycleId, cancellationToken);

        var land = seedingCycle is null ? null : await context.Lands
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == seedingCycle.LandId, cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var farmerName = land is null ? string.Empty :
            users.FirstOrDefault(u => u.Id == land.FarmerId)?.FullName ?? string.Empty;

        return listing.ToDto(farmerName);
    }
}