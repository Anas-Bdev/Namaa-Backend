using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Application.Features.Investments.Queries.GetInvestmentProjectById;
using Namaa.Application.Features.Investments.Queries.GetInvestmentProjectContributionsByIdj;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetInvestmentProjectContributionsById;

public class GetInvestmentProjectContributionsByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetInvestmentProjectContributionsByIdQuery, Result<List<InvestorContributionListItemDto>>>
{
    public async Task<Result<List<InvestorContributionListItemDto>>> Handle(GetInvestmentProjectContributionsByIdQuery request, CancellationToken cancellationToken)
    {
         var contributions = await context.InvestorContributions.AsNoTracking()
            .Include(x => x.InvestmentProject)
            .Where(x => x.InvestmentProjectId == request.ProjectId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return contributions.ToDtos();
    }
}