using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.UpdateInvestmentProject;
public sealed record UpdateInvestmentProjectCommand(
    Guid ProjectId,
    string Title,
    string Description,
    string? CoverImageUrl,
    decimal RequiredAmount,
    decimal MinimumInvestment,
    DateTime FundingDeadline,
    decimal ExpectedRevenue,
    decimal ExpectedCost,
    decimal InvestorProfitSharePercentage,
    int DurationInMonths,
    DateTime? ExpectedStartDate,
    DateTime? ExpectedEndDate
) : IRequest<Result<Updated>>;