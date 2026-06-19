using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.ApproveInvestorContribution;

public class ApproveInvestorContributionCommandHandler(IAppDbContext context, INotificationService notificationService) : IRequestHandler<ApproveInvestorContributionCommand, Result<Updated>>
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
        await notificationService.SendNotificationAsync(
        userId: contribution.InvestorId,
        title: "Investment Approved",
        message: $"Your contribution to the project has been approved by the farmer. Please proceed with the payment.",
        type: "InvestmentApproved",
        referencedId: contribution.Id
        );

        return Result.Updated;
    }
}