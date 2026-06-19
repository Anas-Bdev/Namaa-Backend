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

    var query = from trader in context.TraderProfiles.AsNoTracking()
                 .Include(x => x.Governorate)
                join user in usersQuery on trader.Id equals user.Id
                where user.Status == UserStatus.Active
                select new 
                { 
                    trader, 
                    user.FirstName, 
                    user.LastName, 
                    user.ProfileImageUrl 
                };

    if (request.CityId.HasValue)
        query = query.Where(x => x.trader.GovernorateId == request.CityId);

    if (request.TraderType.HasValue)
        query = query.Where(x => x.trader.BusinessType == request.TraderType);

    var totalCount = await query.CountAsync(cancellationToken);
    var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

    var rawItems = await query
        .OrderBy(x => x.trader.BusinessName)
        .ThenBy(x => x.trader.Id)
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToListAsync(cancellationToken);

    var items = rawItems.Select(x => new TraderListItemDto
    {
        Id = x.trader.Id,
        FullName = string.IsNullOrWhiteSpace(x.LastName) 
            ? x.FirstName 
            : $"{x.FirstName} {x.LastName}".Trim(),
        BusinessName = x.trader.BusinessName!,
        BusinessType = x.trader.BusinessType.ToString(),
        Governorate = x.trader.Governorate!.Name!,
        ProfileImageUrl = x.ProfileImageUrl
    }).ToList();

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