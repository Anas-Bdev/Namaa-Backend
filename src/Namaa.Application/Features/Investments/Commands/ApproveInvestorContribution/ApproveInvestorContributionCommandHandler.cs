using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.ApproveInvestorContribution;

public class ApproveInvestorContributionCommandHandler(IAppDbContext context) : IRequestHandler<ApproveInvestorContributionCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ApproveInvestorContributionCommand request, CancellationToken cancellationToken)
    {
        var contribution=await context.InvestorContributions.FirstOrDefaultAsync(x => x.Id==request.ContributionId,cancellationToken);
        if(contribution is null)
        return ApplicationErrors.InvestorContributionNotFound;
        var investmentProject=await context.InvestmentProjects.Include(x => x.Contributions).FirstOrDefaultAsync(x => x.Id==contribution.InvestmentProjectId,cancellationToken);
        if(investmentProject is null)
        return ApplicationErrors.InvestmentProjectNotFound;
        var approveResult=contribution.Approve();
        if(approveResult.IsError)
        return approveResult.Errors;
        if(investmentProject.AmountCollected >= investmentProject.RequiredAmount)
        {
            var fundResult=investmentProject.MarkAsFunded();
            if(fundResult.IsError)
            return fundResult.Errors;
        }
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;

    }
}