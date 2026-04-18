using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Application.Features.Marketplace.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Queries.GetMySales;

public class GetMySalesQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetMySalesQuery, Result<PaginatedList<OrderDto>>>
{
    public async Task<Result<PaginatedList<OrderDto>>> Handle(
        GetMySalesQuery request,
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

        var query = context.Orders
            .AsNoTracking()
            .Include(o => o.Listing)
            .Where(o => farmerListings.Contains(o.ListingId));

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var orders = await query
            .OrderByDescending(o => o.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);

        var items = orders.Select(o =>
        {
            var traderName = users.FirstOrDefault(u => u.Id == o.TraderId)?.FullName ?? string.Empty;
            return o.ToDto(traderName, o.Listing?.Title ?? string.Empty);
        }).ToList();

        return new PaginatedList<OrderDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = items
        };
    }
}