using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.CompleteInvestmentProject;

public class CompleteInvestmentProjectCommandHandler(IAppDbContext context) : IRequestHandler<CompleteInvestmentProjectCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(CompleteInvestmentProjectCommand request, CancellationToken cancellationToken)
    {
          var investmentProject = await context.InvestmentProjects
            .FindAsync([request.ProjectId], cancellationToken);

        if (investmentProject is null)
            return ApplicationErrors.InvestmentProjectNotFound;

        var completeResult = investmentProject.Complete(
            request.ActualRevenue,
            request.ActualCost
        );

        if (completeResult.IsError)
            return completeResult.Errors;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
    }
