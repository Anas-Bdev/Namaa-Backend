using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.CancelInvestmentProject;

public class CancelInvestmentProjectCommandHandler(IAppDbContext context,INotificationService notificationService) : IRequestHandler<CancelInvestmentProjectCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(CancelInvestmentProjectCommand request, CancellationToken cancellationToken)
    {
        var investmentProject = await context.InvestmentProjects
            .Include(p => p.Contributions)
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

        if(investmentProject is null)
        return ApplicationErrors.InvestmentProjectNotFound;
        var cancelResult=investmentProject.Cancel();
        if(cancelResult.IsError)
        return cancelResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        foreach (var contribution in investmentProject.Contributions)
        {
           await notificationService.SendNotificationAsync(
                userId: contribution.InvestorId,
                title: "Project Cancelled 🛑",
                message: $"The farmer has cancelled the investment project '{investmentProject.Title}'.",
                type: "ProjectCancelled",
                referencedId: investmentProject.Id
            );
        }
        return Result.Updated;
    }
}