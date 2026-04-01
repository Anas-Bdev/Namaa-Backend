using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Investment;

namespace Namaa.Application.Features.Investments.Mappers;

public static class InvestmentMapper
{
    public static InvestmentProjectDto ToDto(
        this InvestmentProject project,
        string creatorName,
        Dictionary<Guid, string> contributorNames)
    {
        return new InvestmentProjectDto
        {
            Id = project.Id,
            CreatorId = project.CreatorId,
            CreatorName = creatorName,
            CreatorRole = project.CreatorRole.ToString(),
            Title = project.Title,
            Description = project.Description,
            RequiredAmount = project.RequiredAmount,
            AmountCollected = project.AmountCollected,
            ExpectedProfit = project.ExpectedProfit,
            SharePercentage = project.SharePercentage,
            Status = project.Status.ToString(),
            Contributions = project.Contributions
                .Select(c => c.ToDto(contributorNames.GetValueOrDefault(c.ContributorId, string.Empty)))
                .ToList()
        };
    }

    public static InvestmentProjectSummaryDto ToSummaryDto(
        this InvestmentProject project,
        string creatorName)
    {
        return new InvestmentProjectSummaryDto
        {
            Id = project.Id,
            CreatorId = project.CreatorId,
            CreatorName = creatorName,
            CreatorRole = project.CreatorRole.ToString(),
            Title = project.Title,
            RequiredAmount = project.RequiredAmount,
            AmountCollected = project.AmountCollected,
            Status = project.Status.ToString()
        };
    }

    public static ContributionDto ToDto(
        this InvestorContribution contribution,
        string contributorName)
    {
        return new ContributionDto
        {
            Id = contribution.Id,
            ContributorId = contribution.ContributorId,
            ContributorName = contributorName,
            Amount = contribution.Amount,
            Status = contribution.Status.ToString()
        };
    }
}