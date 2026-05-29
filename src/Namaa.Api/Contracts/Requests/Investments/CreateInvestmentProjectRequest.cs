using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.Investments;
public class CreateInvestmentProjectRequest
{
    [Required(ErrorMessage = "Land is required.")]
    public Guid LandId { get; init; }

    [Required(ErrorMessage = "Project title is required.")]
    public string Title { get; init; } = default!;

    [Required(ErrorMessage = "Project description is required.")]
    public string Description { get; init; } = default!;

    public string? CoverImageUrl { get; init; }

    [Required(ErrorMessage = "Required amount is required.")]
    public decimal RequiredAmount { get; init; }

    [Required(ErrorMessage = "Minimum investment is required.")]
    public decimal MinimumInvestment { get; init; }

    [Required(ErrorMessage = "Funding deadline is required.")]
    public DateTime FundingDeadline { get; init; }

    [Required(ErrorMessage = "Expected revenue is required.")]
    public decimal ExpectedRevenue { get; init; }

    [Required(ErrorMessage = "Expected cost is required.")]
    public decimal ExpectedCost { get; init; }

    [Required(ErrorMessage = "Investor profit share percentage is required.")]
    public decimal InvestorProfitSharePercentage { get; init; }

    [Required(ErrorMessage = "Duration in months is required.")]
    public int DurationInMonths { get; init; }
    public DateTime? ExpectedStartDate { get; init; }
    public DateTime? ExpectedEndDate { get; init; }
}