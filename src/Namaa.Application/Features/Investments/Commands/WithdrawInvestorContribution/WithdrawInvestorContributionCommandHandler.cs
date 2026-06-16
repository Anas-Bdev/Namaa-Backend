using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.WithdrawInvestorContribution;

public class WithdrawInvestorContributionCommandHandler(IAppDbContext context) : IRequestHandler<WithdrawInvestorContributionCommand, Result<Updated>>
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
         
         return Result.Updated;


    }
}