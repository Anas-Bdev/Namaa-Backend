namespace Namaa.Application.Features.Investments.Dtos;

public class ContributionDto
{
    public Guid Id { get; set; }
    public Guid ContributorId { get; set; }
    public string ContributorName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal SharePercentage { get; set; }
    public decimal? ProfitShare { get; set; }
    public string Status { get; set; } = string.Empty;
}