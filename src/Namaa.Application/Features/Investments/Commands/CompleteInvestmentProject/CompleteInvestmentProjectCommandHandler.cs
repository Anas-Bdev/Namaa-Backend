using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.CompleteInvestmentProject;

public class CompleteInvestmentProjectCommandHandler(IAppDbContext context, INotificationService notificationService) : IRequestHandler<CompleteInvestmentProjectCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(CompleteInvestmentProjectCommand request, CancellationToken cancellationToken)
    {
          var investmentProject = await context.InvestmentProjects
            .Include(p => p.Contributions)
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

        if (investmentProject is null)
            return ApplicationErrors.InvestmentProjectNotFound;

        var completeResult = investmentProject.Complete(
            request.ActualRevenue,
            request.ActualCost
        );

        if (completeResult.IsError)
        return completeResult.Errors;

        await context.SaveChangesAsync(cancellationToken);

        foreach (var contribution in investmentProject.Contributions)
        {
            await notificationService.SendNotificationAsync(
                userId: contribution.InvestorId,
                title: "Project Completed! 🎉",
                message: $"Great news! The farmer has successfully completed the investment project '{investmentProject.Title}'.",
                type: "ProjectCompleted",
                referencedId: investmentProject.Id
            );
        }
        return Result.Updated;
    }
    }
