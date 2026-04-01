namespace Namaa.Application.Features.Investments.Dtos;

public class InvestmentProjectSummaryDto
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public string CreatorName { get; set; } = string.Empty;
    public string CreatorRole { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal RequiredAmount { get; set; }
    public decimal AmountCollected { get; set; }
    public string Status { get; set; } = string.Empty;
}