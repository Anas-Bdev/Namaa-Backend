using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetMyContributions;

public class GetMyContributionsQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetMyContributionsQuery, Result<PaginatedList<ContributionDto>>>
{
    public async Task<Result<PaginatedList<ContributionDto>>> Handle(
        GetMyContributionsQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.InvestorContributions
            .AsNoTracking()
            .Where(c => c.ContributorId == request.UserId);

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var contributions = await query
            .OrderByDescending(c => c.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);

        var items = contributions.Select(c =>
            c.ToDto(users.FirstOrDefault(u => u.Id == c.ContributorId)?.FullName ?? string.Empty))
            .ToList();

        return new PaginatedList<ContributionDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = items
        };
    }
}
