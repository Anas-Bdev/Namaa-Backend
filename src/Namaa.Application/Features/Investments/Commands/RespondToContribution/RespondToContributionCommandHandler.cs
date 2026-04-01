using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Application.Features.Investments.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Investment;

namespace Namaa.Application.Features.Investments.Commands.RespondToContribution;

public class RespondToContributionCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<RespondToContributionCommand, Result<ContributionDto>>
{
    public async Task<Result<ContributionDto>> Handle(
        RespondToContributionCommand request,
        CancellationToken cancellationToken)
    {
        var contribution = await context.InvestorContributions
            .Include(c => c.Project)
            .FirstOrDefaultAsync(c => c.Id == request.ContributionId, cancellationToken);

        if (contribution is null)
            return ApplicationErrors.ContributionNotFound;

        if (contribution.Project!.CreatorId != request.UserId)
            return InvestorContributionErrors.Unauthorized;

        var result = request.IsApproved
            ? contribution.Approve()
            : contribution.Reject();
        
        if (result.IsError)
            return result.Errors;

        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var contributorName = users.FirstOrDefault(u => u.Id == contribution.ContributorId)?.FullName ?? string.Empty;

        return contribution.ToDto(contributorName);
    }
}