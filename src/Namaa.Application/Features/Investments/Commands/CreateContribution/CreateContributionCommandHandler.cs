using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Investment;

namespace Namaa.Application.Features.Investments.Commands.CreateContribution;

public class CreateContributionCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<CreateContributionCommand, Result<ContributionDto>>
{
    public async Task<Result<ContributionDto>> Handle(
        CreateContributionCommand request,
        CancellationToken cancellationToken)
    {
        var project = await context.InvestmentProjects
            .Include(p => p.Contributions)
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

        if (project is null)
            return ApplicationErrors.InvestmentProjectNotFound;

        if (project.CreatorId == request.ContributorId)
            return InvestmentProjectErrors.CannotContributeToOwnProject;

        var contribution = InvestorContribution.Create(
            Guid.NewGuid(),
            request.ProjectId,
            request.ContributorId,
            request.Amount
        );

        if (contribution.IsError)
            return contribution.Errors;

        var addResult = project.AddContribution(contribution.Value);
        if (addResult.IsError)
            return addResult.Errors;

        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var contributorName = users.FirstOrDefault(u => u.Id == request.ContributorId)?.FullName ?? string.Empty;

        return contribution.Value.ToDto(contributorName);
    }
}