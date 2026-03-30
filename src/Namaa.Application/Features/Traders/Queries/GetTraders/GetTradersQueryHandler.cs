using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Traders.Queries.GetTraders;

public class GetTradersQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetTradersQuery, Result<PaginatedList<TraderSummaryDto>>>
{
    public async Task<Result<PaginatedList<TraderSummaryDto>>> Handle(
        GetTradersQuery request,
        CancellationToken cancellationToken)
    {
        var tradersQuery = context.TraderProfiles.AsNoTracking();

        if (request.CityId.HasValue)
            tradersQuery = tradersQuery.Where(x => x.CityId == request.CityId);

        var totalCount = await tradersQuery.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var traders = await tradersQuery
            .OrderByDescending(x => x.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var users = await userReadRepository.Query()
            .ToListAsync(cancellationToken);

        var items = traders.Select(trader => new TraderSummaryDto
        {
            Id = trader.Id,
            FullName = users.FirstOrDefault(u => u.Id == trader.Id)?.FullName ?? string.Empty,
            BusinessName = trader.BusinessName,
            BusinessType = trader.BusinessType,
            CityId = trader.CityId ?? 0
        }).ToList();

        return new PaginatedList<TraderSummaryDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = items
        };
    }
}

