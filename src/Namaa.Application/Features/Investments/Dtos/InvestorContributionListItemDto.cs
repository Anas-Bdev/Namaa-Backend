using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investments.Dtos;
public class InvestorContributionListItemDto
{
    public Guid Id { get; set; }
    public Guid InvestorId {get;set;}
    public Guid InvestmentProjectId { get; set; }

    public decimal Amount { get; set; }
    public decimal? ProfitAmount { get; set; }
    public ContributionStatus Status { get; set; }

    public string ProjectTitle { get; set; } = string.Empty;
    public string? ProjectCoverImageUrl { get; set; }
    public InvestmentProjectStatus ProjectStatus { get; set; }
}