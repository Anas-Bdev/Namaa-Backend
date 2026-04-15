using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Investment;

namespace Namaa.Application.Features.Investments.Queries.GetProjectById;

public class GetProjectByIdQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetProjectByIdQuery, Result<InvestmentProjectDto>>
{
    public async Task<Result<InvestmentProjectDto>> Handle(
        GetProjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        var project = await context.InvestmentProjects
            .AsNoTracking()
            .Include(p => p.Contributions)
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

        if (project is null)
            return ApplicationErrors.InvestmentProjectNotFound;

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var creatorName = users.FirstOrDefault(u => u.Id == project.CreatorId)?.FullName ?? string.Empty;
        var contributorNames = project.Contributions
            .ToDictionary(c => c.ContributorId,
                c => users.FirstOrDefault(u => u.Id == c.ContributorId)?.FullName ?? string.Empty);

        return project.ToDto(creatorName, contributorNames);
    }
}