using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.StartFunding;

public sealed record StartFundingCommand(
    Guid ProjectId,
    Guid UserId
) : IRequest<Result<InvestmentProjectDto>>;
