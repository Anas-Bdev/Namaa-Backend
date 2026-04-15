using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investors.Queries.GetInvestors;

public class GetInvestorsQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetInvestorsQuery, Result<PaginatedList<InvestorSummaryDto>>>
{
    public async Task<Result<PaginatedList<InvestorSummaryDto>>> Handle(
        GetInvestorsQuery request,
        CancellationToken cancellationToken)
    {
        var investorsQuery = context.InvestorProfiles.AsNoTracking();

        if (request.CityId.HasValue)
            investorsQuery = investorsQuery.Where(x => x.CityId == request.CityId);

        var totalCount = await investorsQuery.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var investors = await investorsQuery
            .OrderByDescending(x => x.CreatedAtUtc)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var users = await userReadRepository.Query()
            .ToListAsync(cancellationToken);

        var items = investors.Select(investor => new InvestorSummaryDto
        {
            Id = investor.Id,
            FullName = users.FirstOrDefault(u => u.Id == investor.Id)?.FullName ?? string.Empty,
            OrganizationName = investor.OrganizationName,
            CompanyName = investor.CompanyName,
            CityId = investor.CityId ?? 0
        }).ToList();

        return new PaginatedList<InvestorSummaryDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = items
        };
    }
}