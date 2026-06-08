using MediatR;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Commands.CreateInvestmentProject;
public sealed record CreateInvestmentProjectCommand(
    Guid FarmerId,
    Guid LandId,
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
) : IRequest<Result<InvestmentProjectDto>>;