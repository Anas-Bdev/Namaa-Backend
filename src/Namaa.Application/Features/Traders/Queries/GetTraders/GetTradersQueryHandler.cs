using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Traders.Queries.GetTraders;
public class GetTradersQueryHandler(IAppDbContext context, IUserReadRepository userReadRepository) 
    : IRequestHandler<GetTradersQuery, Result<PaginatedList<TraderListItemDto>>>
{
    public async Task<Result<PaginatedList<TraderListItemDto>>> Handle(GetTradersQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = userReadRepository.Query();

        // 1. Build the base join query
        var query = from trader in context.TraderProfiles.AsNoTracking()
                    join user in usersQuery on trader.Id equals user.Id
                    where user.Status == UserStatus.Active // Only show active traders
                    select new { trader, user };

        // 2. Apply Filters
        // Filter by Governorate (City)
        if (request.CityId.HasValue)
        {
            query = query.Where(x => x.trader.GovernorateId == request.CityId);
        }

        // Filter by Trader Type (e.g., Exporter, Wholesaler)
        if (request.TraderType.HasValue)
        {
            query = query.Where(x => x.trader.BusinessType == request.TraderType);
        }

        // 3. Calculate Pagination Metadata
        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        // 4. Apply Sorting, Pagination, and Projection
        var items = await query
            .OrderBy(x => x.trader.BusinessName) // Traders are sorted by Business Name, not Full Name
            .ThenBy(x => x.trader.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new TraderListItemDto
            {
                Id = x.trader.Id,
                FullName = x.user.FullName,
                BusinessName = x.trader.BusinessName!,
                BusinessType = x.trader.BusinessType.ToString(), 
                Governorate = x.trader.Governorate!.Name!,
                ProfileImageUrl=x.user.ProfileImageUrl
            })
            .ToListAsync(cancellationToken);

        // 5. Return the result
        return new PaginatedList<TraderListItemDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = items
        };
    }
}