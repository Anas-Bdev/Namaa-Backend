using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.Investments;
public class UpdateInvestmentProjectRequest
{
    [Required(ErrorMessage = "Project title is required.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Project description is required.")]
    public string Description { get; set; } = string.Empty;

    public string? CoverImageUrl { get; set; }

    [Required(ErrorMessage = "Required amount is required.")]
    public decimal RequiredAmount { get; set; }

    [Required(ErrorMessage = "Minimum investment is required.")]
    public decimal MinimumInvestment { get; set; }

    [Required(ErrorMessage = "Funding deadline is required.")]
    public DateTime FundingDeadline { get; set; }

    [Required(ErrorMessage = "Expected revenue is required.")]
    public decimal ExpectedRevenue { get; set; }

    [Required(ErrorMessage = "Expected cost is required.")]
    public decimal ExpectedCost { get; set; }

    [Required(ErrorMessage = "Investor profit share percentage is required.")]
    public decimal InvestorProfitSharePercentage { get; set; }

    [Required(ErrorMessage = "Duration in months is required.")]
    public int DurationInMonths { get; set; }

    public DateTime? ExpectedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
}