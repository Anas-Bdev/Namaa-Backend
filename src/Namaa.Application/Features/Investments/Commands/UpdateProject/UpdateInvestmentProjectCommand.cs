using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.UpdateProject;

public sealed record UpdateInvestmentProjectCommand(
    Guid ProjectId,
    Guid UserId,
    string Title,
    string? Description,
    decimal RequiredAmount,
    decimal? ExpectedProfit,
    decimal? SharePercentage
) : IRequest<Result<InvestmentProjectDto>>;