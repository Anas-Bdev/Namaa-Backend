using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Application.Features.Marketplace.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Queries.GetMyOrders;

public class GetMyOrdersQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetMyOrdersQuery, Result<PaginatedList<OrderDto>>>
{
    public async Task<Result<PaginatedList<OrderDto>>> Handle(
        GetMyOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Orders
            .AsNoTracking()
            .Include(o => o.Listing)
            .Where(o => o.TraderId == request.TraderId);

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var orders = await query
            .OrderByDescending(o => o.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var traderName = users.FirstOrDefault(u => u.Id == request.TraderId)?.FullName ?? string.Empty;

        var items = orders.Select(o => o.ToDto(traderName, o.Listing?.Title ?? string.Empty)).ToList();

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