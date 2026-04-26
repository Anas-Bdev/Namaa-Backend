using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.CancelInvestmentProject;

public class CancelInvestmentProjectCommandHandler(IAppDbContext context) : IRequestHandler<CancelInvestmentProjectCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(CancelInvestmentProjectCommand request, CancellationToken cancellationToken)
    {
        var investmentProject=await context.InvestmentProjects.FindAsync([request.ProjectId],cancellationToken);
        if(investmentProject is null)
        return ApplicationErrors.InvestmentProjectNotFound;
        var cancelResult=investmentProject.Cancel();
        if(cancelResult.IsError)
        return cancelResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}