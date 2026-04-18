using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Application.Features.Marketplace.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Queries.GetAllListings;

public class GetAllListingsQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetAllListingsQuery, Result<PaginatedList<ProductListingSummaryDto>>>
{
    public async Task<Result<PaginatedList<ProductListingSummaryDto>>> Handle(
        GetAllListingsQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.ProductListings.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var listings = await query
            .OrderByDescending(l => l.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var seedingCycleIds = listings.Select(l => l.SeedingCycleId).ToList();
        var seedingCycles = await context.SeedingCycles
            .AsNoTracking()
            .Where(s => seedingCycleIds.Contains(s.Id))
            .ToListAsync(cancellationToken);

        var landIds = seedingCycles.Select(s => s.LandId).ToList();
        var lands = await context.Lands
            .AsNoTracking()
            .Where(l => landIds.Contains(l.Id))
            .ToListAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);

        var items = listings.Select(listing =>
        {
            var cycle = seedingCycles.FirstOrDefault(s => s.Id == listing.SeedingCycleId);
            var land = cycle is null ? null : lands.FirstOrDefault(l => l.Id == cycle.LandId);
            var farmerName = land is null ? string.Empty :
                users.FirstOrDefault(u => u.Id == land.FarmerId)?.FullName ?? string.Empty;
            return listing.ToSummaryDto(farmerName);
        }).ToList();

        return new PaginatedList<ProductListingSummaryDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = items
        };
    }
}