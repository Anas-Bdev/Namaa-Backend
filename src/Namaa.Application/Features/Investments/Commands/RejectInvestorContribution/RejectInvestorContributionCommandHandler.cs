using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.RejectInvestorContribution;

public class RejectInvestorContributionCommandHandler(IAppDbContext context, INotificationService notificationService) : IRequestHandler<RejectInvestorContributionCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(RejectInvestorContributionCommand request, CancellationToken cancellationToken)
    {
        var contribution = await context.InvestorContributions
            .FirstOrDefaultAsync(x => x.Id == request.ContributionId, cancellationToken);

        if (contribution is null)
            return ApplicationErrors.InvestorContributionNotFound;

        var rejectResult = contribution.Reject();

        if (rejectResult.IsError)
            return rejectResult.Errors;

        await context.SaveChangesAsync(cancellationToken);
        await notificationService.SendNotificationAsync(
        userId: contribution.InvestorId,
        title: "Investment Update",
        message: $"Your contribution offer was declined by the farmer.",
        type: "InvestmentRejected",
        referencedId: contribution.Id
        );
        return Result.Updated;
    }
}