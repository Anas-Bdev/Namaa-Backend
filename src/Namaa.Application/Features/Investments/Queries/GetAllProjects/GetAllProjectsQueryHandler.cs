using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetAllProjectsQuery, Result<PaginatedList<InvestmentProjectSummaryDto>>>
{
    public async Task<Result<PaginatedList<InvestmentProjectSummaryDto>>> Handle(
        GetAllProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.InvestmentProjects.AsNoTracking();

        if (request.CreatorRole.HasValue)
            query = query.Where(p => p.CreatorRole == request.CreatorRole);

        if (request.Status.HasValue)
            query = query.Where(p => p.Status == request.Status);

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var projects = await query
            .OrderByDescending(p => p.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);

        var items = projects.Select(p =>
            p.ToSummaryDto(users.FirstOrDefault(u => u.Id == p.CreatorId)?.FullName ?? string.Empty))
            .ToList();

        return new PaginatedList<InvestmentProjectSummaryDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = items
        };
    }
}