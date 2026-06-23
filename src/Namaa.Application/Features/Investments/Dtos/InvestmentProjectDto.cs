using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investments.Dtos;
public class InvestmentProjectDto
{
    public Guid FarmerId {get;set;}
    public Guid LandId {get;set;}
     public Guid Id { get; set; }
     public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public decimal RequiredAmount { get; set; }
    public decimal MinimumInvestment { get; set; }
    public decimal AmountCollected { get; set; }
    public DateTime FundingDeadline { get; set; }
    public decimal ExpectedRevenue { get; set; }
    public decimal ExpectedCost { get; set; }
    public decimal ExpectedProfit { get; set; }
    public decimal InvestorProfitSharePercentage { get; set; }
    public decimal? ActualRevenue { get; set; }
    public decimal? ActualCost { get; set; }
    public decimal? ActualProfit { get; set; }
    public int DurationInMonths { get; set; }
    public DateTime? ExpectedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
    public string Status { get; set; }=string.Empty;
    
}