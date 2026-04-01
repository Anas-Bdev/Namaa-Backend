using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investments.Commands.CreateProject;

public sealed record CreateInvestmentProjectCommand(
    Guid CreatorId,
    ProjectCreatorRole CreatorRole,
    string Title,
    string? Description,
    decimal RequiredAmount,
    decimal? ExpectedProfit,
    decimal? SharePercentage
) : IRequest<Result<InvestmentProjectDto>>;