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

        var approveResult = contribution.Approve();

        if (approveResult.IsError)
        return approveResult.Errors;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}