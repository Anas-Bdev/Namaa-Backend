using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Investment;

namespace Namaa.Application.Features.Investments.Commands.StartWork;

public class StartWorkCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<StartWorkCommand, Result<InvestmentProjectDto>>
{
    public async Task<Result<InvestmentProjectDto>> Handle(
        StartWorkCommand request,
        CancellationToken cancellationToken)
    {
        var project = await context.InvestmentProjects
            .Include(p => p.Contributions)
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

        if (project is null)
            return ApplicationErrors.InvestmentProjectNotFound;

        if (project.CreatorId != request.UserId)
            return InvestmentProjectErrors.Unauthorized;

        var result = project.StartWork();
        if (result.IsError)
            return result.Errors;

        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var creatorName = users.FirstOrDefault(u => u.Id == project.CreatorId)?.FullName ?? string.Empty;
        var contributorNames = project.Contributions
            .ToDictionary(c => c.ContributorId,
                c => users.FirstOrDefault(u => u.Id == c.ContributorId)?.FullName ?? string.Empty);

        return project.ToDto(creatorName, contributorNames);
    }
}