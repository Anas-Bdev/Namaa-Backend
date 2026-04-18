using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Application.Features.Marketplace.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Queries.GetFarmerRatings;

public class GetFarmerRatingsQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetFarmerRatingsQuery, Result<PaginatedList<FarmerRatingDto>>>
{
    public async Task<Result<PaginatedList<FarmerRatingDto>>> Handle(
        GetFarmerRatingsQuery request,
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

        var farmerListings = await context.ProductListings
            .AsNoTracking()
            .Where(l => farmerCycles.Contains(l.SeedingCycleId))
            .Select(l => l.Id)
            .ToListAsync(cancellationToken);

        var farmerOrders = await context.Orders
            .AsNoTracking()
            .Where(o => farmerListings.Contains(o.ListingId))
            .Select(o => o.Id)
            .ToListAsync(cancellationToken);

        var query = context.FarmerRatings
            .AsNoTracking()
            .Where(r => farmerOrders.Contains(r.OrderId));

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var ratings = await query
            .OrderByDescending(r => r.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);

        var items = ratings.Select(r =>
        {
            var traderName = users.FirstOrDefault(u => u.Id == r.ReviewerTraderId)?.FullName ?? string.Empty;
            return r.ToDto(traderName);
        }).ToList();

        return new PaginatedList<FarmerRatingDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = items
        };
    }
}