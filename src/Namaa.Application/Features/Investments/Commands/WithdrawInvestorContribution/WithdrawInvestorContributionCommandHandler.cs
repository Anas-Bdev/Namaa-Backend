using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.WithdrawInvestorContribution;

public class WithdrawInvestorContributionCommandHandler(IAppDbContext context, INotificationService notificationService) : IRequestHandler<WithdrawInvestorContributionCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(WithdrawInvestorContributionCommand request, CancellationToken cancellationToken)
    {
        var project = await context.InvestmentProjects
            .Include(p => p.Contributions)
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

        if (project is null)
        return ApplicationErrors.InvestmentProjectNotFound;

        var contribution = project.Contributions.FirstOrDefault(c => c.Id == request.ContributionId);

        if (contribution is null)
        return ApplicationErrors.InvestorContributionNotFound;

        if (contribution.InvestorId != request.InvestorId)
         return ApplicationErrors.Forbidden;

         var result = contribution.Withdraw();

         if(result.IsError)
         return result.Errors;

         await context.SaveChangesAsync(cancellationToken);

         await notificationService.SendNotificationAsync(
         userId: project.FarmerId,
         title: "Investment Withdrawn",
         message: $"An investor has withdrawn their contribution offer from your project.",
         type: "InvestmentWithdrawn",
         referencedId: contribution.InvestmentProjectId
         );
         return Result.Updated;


    }
}