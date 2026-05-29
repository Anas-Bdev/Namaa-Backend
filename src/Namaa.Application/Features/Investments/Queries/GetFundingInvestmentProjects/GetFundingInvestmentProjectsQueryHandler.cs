using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investments.Queries.GetFundingInvestmentProjects;

public class GetFundingInvestmentProjectsQueryHandler(IAppDbContext context) : IRequestHandler<GetFundingInvestmentProjectsQuery, Result<List<InvestmentProjectListItemDto>>>{
    public async Task<Result<List<InvestmentProjectListItemDto>>> Handle(GetFundingInvestmentProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects=await context.InvestmentProjects.AsNoTracking()
                                  .Where(x => x.Status==InvestmentProjectStatus.Funding)
                                  .Include(x => x.Contributions)
                                  .OrderByDescending(x => x.FundingDeadline)
                                  .ToListAsync(cancellationToken);
       return projects.ToDtos();
    }
}