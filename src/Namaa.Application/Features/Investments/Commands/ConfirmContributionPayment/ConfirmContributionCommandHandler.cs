using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Investments;

namespace Namaa.Application.Features.Investments.Commands.ConfirmContributionPayment;

public class ConfirmContributionPaymentCommandHandler(IAppDbContext context) : IRequestHandler<ConfirmContributionPaymentCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ConfirmContributionPaymentCommand request, CancellationToken cancellationToken)
    {
        var project = await context.InvestmentProjects
            .Include(p => p.Contributions)
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

        if (project is null)
            return ApplicationErrors.InvestmentProjectNotFound;
        
        var result = project.ProcessPaymentForContribution(request.ContributionId);

        if(result.IsError)
        return result.Errors;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}