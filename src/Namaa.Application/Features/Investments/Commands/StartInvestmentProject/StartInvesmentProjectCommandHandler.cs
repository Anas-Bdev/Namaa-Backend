using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.StartInvestmentProject;

public class StartInvestmentProjectCommandHandler(IAppDbContext context) : IRequestHandler<StartInvestmentProjectCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(StartInvestmentProjectCommand request, CancellationToken cancellationToken)
    {
        var investmentProject=await context.InvestmentProjects.FindAsync([request.ProjectId],cancellationToken);

        if (investmentProject is null)
            return ApplicationErrors.InvestmentProjectNotFound;

        var startResult = investmentProject.StartProgress();

        if (startResult.IsError)
            return startResult.Errors;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}