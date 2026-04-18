using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Application.Features.Marketplace.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Queries.GetMyListings;

public class GetMyListingsQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetMyListingsQuery, Result<PaginatedList<ProductListingSummaryDto>>>
{
    public async Task<Result<PaginatedList<ProductListingSummaryDto>>> Handle(
        GetMyListingsQuery request,
        CancellationToken cancellationToken)
    {
        var farmerLands = await context.Lands
            .AsNoTracking()
            .Where(l => l.FarmerId == request.FarmerId)
            .Select(l => l.Id)
            .ToListAsync(cancellationToken);

        var farmerCycles = await context.SeedingCycles
            .AsNoTracking()
            .Where(s => farmerLands.Contains(s.LandId))
            .Select(s => s.Id)
            .ToListAsync(cancellationToken);

        var query = context.ProductListings
            .AsNoTracking()
            .Where(l => farmerCycles.Contains(l.SeedingCycleId));

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var listings = await query
            .OrderByDescending(l => l.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var farmerName = users.FirstOrDefault(u => u.Id == request.FarmerId)?.FullName ?? string.Empty;

        var items = listings.Select(l => l.ToSummaryDto(farmerName)).ToList();

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