using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.SetActualResults;

public sealed record SetActualResultsCommand(
    Guid ProjectId,
    Guid UserId,
    decimal ActualRevenue,
    decimal ActualCost
) : IRequest<Result<InvestmentProjectDto>>;