using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.UpdateInvestmentProject;

public class UpdateInvestmentProjectCommandHandler(IAppDbContext context) : IRequestHandler<UpdateInvestmentProjectCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateInvestmentProjectCommand request, CancellationToken cancellationToken)
    {
        var investmentProject=await context.InvestmentProjects.FindAsync([request.ProjectId],cancellationToken);
        if(investmentProject is null)
        return ApplicationErrors.InvestmentProjectNotFound;

        var updateResult=investmentProject.Update(
            request.Title,
            request.Description,request.CoverImageUrl,
            request.RequiredAmount,
            request.MinimumInvestment,
            request.FundingDeadline,
            request.ExpectedRevenue,
            request.ExpectedCost,
            request.InvestorProfitSharePercentage,
            request.DurationInMonths,
            request.ExpectedStartDate,
            request.ExpectedEndDate
        );
        
        if(updateResult.IsError)
        return updateResult.Errors;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}