using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investments.Dtos;

public class InvestmentProjectDto
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public string CreatorName { get; set; } = string.Empty;
    public string CreatorRole { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal RequiredAmount { get; set; }
    public decimal AmountCollected { get; set; }
    public decimal? ExpectedProfit { get; set; }
    public decimal? SharePercentage { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<ContributionDto> Contributions { get; set; } = [];
}