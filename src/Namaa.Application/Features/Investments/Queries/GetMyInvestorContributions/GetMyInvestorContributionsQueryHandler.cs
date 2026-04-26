using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Investments;

namespace Namaa.Application.Features.Investments.Queries.GetMyInvestorContributions;

public class GetMyInvestorContributionsQueryHandler(IAppDbContext context) : IRequestHandler<GetMyInvestorContributionsQuery, Result<List<InvestorContributionListItemDto>>>
{
    public async Task<Result<List<InvestorContributionListItemDto>>> Handle(GetMyInvestorContributionsQuery request, CancellationToken cancellationToken)
    {
        var contributions=await context.InvestorContributions.AsNoTracking()
                                      .Where(x => x.InvestorId==request.InvestorId)
                                      .Include(x => x.InvestmentProject)
                                      .OrderByDescending(x => x.CreatedAtUtc)
                                      .ToListAsync(cancellationToken);
       return contributions.ToDtos();
    }
}