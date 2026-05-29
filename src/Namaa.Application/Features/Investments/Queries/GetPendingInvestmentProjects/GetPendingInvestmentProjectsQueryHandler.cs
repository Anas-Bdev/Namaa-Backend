using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;


using Namaa.Domain.Enums;
using Namaa.Application.Features.Investments.Mappers;
namespace Namaa.Application.Features.Investments.Queries.GetPendingInvestmentProjects;

public class GetPendingInvestmentProjectsQueryHandler(IAppDbContext context) : IRequestHandler<GetPendingInvestmentProjectsQuery, Result<List<InvestmentProjectListItemDto>>>
{
    public async Task<Result<List<InvestmentProjectListItemDto>>> Handle(GetPendingInvestmentProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects=await context.InvestmentProjects.AsNoTracking()
                                  .Where(x => x.Status==InvestmentProjectStatus.Pending)
                                  .Include(x => x.Contributions)
                                  .OrderByDescending(x => x.FundingDeadline)
                                  .ToListAsync(cancellationToken);
        return projects.ToDtos();
                                  
    }
}