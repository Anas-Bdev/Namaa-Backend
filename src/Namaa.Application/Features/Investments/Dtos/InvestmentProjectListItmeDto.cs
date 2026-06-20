using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investments.Dtos;
public class InvestmentProjectListItemDto
{

    public Guid LandId { get; set; }
    public Guid FarmerId { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public decimal RequiredAmount { get; set; }
    public decimal AmountCollected { get; set; }
    public DateTime FundingDeadline { get; set; }
    public decimal ExpectedProfit { get; set; }
    public InvestmentProjectStatus Status { get; set; }
}