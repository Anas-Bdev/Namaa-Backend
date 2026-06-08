using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetInvestmentProjectById;

public sealed record GetInvestmentProjectByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetInvestmentProjectByIdQuery, Result<InvestmentProjectDto>>
{
    public async Task<Result<InvestmentProjectDto>> Handle(GetInvestmentProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var investmentProject=await context.InvestmentProjects.AsNoTracking()
                                           .Include(x => x.Contributions)
                                           .Where(x => x.Id==request.ProjectId)
                                           .FirstOrDefaultAsync(cancellationToken);

        if(investmentProject is null)
        return ApplicationErrors.InvestmentProjectNotFound;
        return investmentProject.ToDto();
    }
}