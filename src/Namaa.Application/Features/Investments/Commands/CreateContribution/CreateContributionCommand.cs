using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.CreateContribution;

public sealed record CreateContributionCommand(
    Guid ProjectId,
    Guid ContributorId,
    decimal Amount
) : IRequest<Result<ContributionDto>>;