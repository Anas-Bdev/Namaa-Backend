using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.Investment;

namespace Namaa.Application.Features.Investments.Commands.DeleteProject;

public class DeleteInvestmentProjectCommandHandler(IAppDbContext context)
    : IRequestHandler<DeleteInvestmentProjectCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(
        DeleteInvestmentProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = await context.InvestmentProjects
            .Include(p => p.Contributions)
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

        if (project is null)
            return ApplicationErrors.InvestmentProjectNotFound;

        if (project.CreatorId != request.UserId)
            return InvestmentProjectErrors.Unauthorized;

        var hasApprovedContributions = project.Contributions
            .Any(c => c.Status == ContributionStatus.Approved);

        if (hasApprovedContributions)
            return InvestmentProjectErrors.CannotDeleteWithContributions;

        context.InvestmentProjects.Remove(project);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}