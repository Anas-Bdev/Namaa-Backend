using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetMyInvestmentProjects;

public class GetMyInvestmentProjectsQueryHandler(IAppDbContext context) : IRequestHandler<GetMyInvestmentProjectsQuery, Result<List<InvestmentProjectListItemDto>>>
{
    public async Task<Result<List<InvestmentProjectListItemDto>>> Handle(GetMyInvestmentProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects=await context.InvestmentProjects.AsNoTracking()
                                  .Where(x => x.FarmerId==request.FarmerId)
                                  .Include(x => x.Contributions)
                                  .OrderByDescending(x => x.FundingDeadline)
                                  .ToListAsync(cancellationToken);
     return projects.ToDtos();
    }
}