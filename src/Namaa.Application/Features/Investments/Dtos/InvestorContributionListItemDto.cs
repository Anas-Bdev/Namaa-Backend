using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investments.Dtos;
public class InvestorContributionListItemDto
{
    public Guid Id { get; set; }
    public Guid InvestorId {get;set;}
    public Guid InvestmentProjectId { get; set; }

    public decimal Amount { get; set; }
    public decimal? ProfitAmount { get; set; }
    public string Status { get; set; }=string.Empty;

    public string ProjectTitle { get; set; } = string.Empty;
    public string? ProjectCoverImageUrl { get; set; }
    public string ProjectStatus { get; set; }=string.Empty;

    public DateTimeOffset CreatedAt {get;set;}
}