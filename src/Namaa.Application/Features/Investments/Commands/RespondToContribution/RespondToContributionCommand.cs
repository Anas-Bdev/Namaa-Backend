using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.RespondToContribution;

public sealed record RespondToContributionCommand(
    Guid ContributionId,
    Guid UserId,
    bool IsApproved
) : IRequest<Result<ContributionDto>>;