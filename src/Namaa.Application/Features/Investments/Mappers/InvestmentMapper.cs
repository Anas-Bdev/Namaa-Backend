using System.Reflection.Metadata;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Investments;

namespace Namaa.Application.Features.Investments.Mappers;
public static class InvestmentMapper
{
    public static InvestorContributionDto ToDto(this InvestorContribution entity)
    {
        return new InvestorContributionDto
        {
            Id=entity.Id,
            InvestmentProjectId=entity.InvestmentProjectId,
            Status=entity.Status,
            Amount=entity.Amount,
            ProfitAmount=entity.ProfitAmount
        };
    }
    public static InvestmentProjectDto ToDto(this InvestmentProject entity)
    {
        return new InvestmentProjectDto
        {
            Id=entity.Id,
            Title=entity.Title,
            Description=entity.Description,
            CoverImageUrl=entity.CoverImageUrl,
            RequiredAmount=entity.RequiredAmount,
            MinimumInvestment=entity.MinimumInvestment,
            AmountCollected=entity.AmountCollected,
            FundingDeadline=entity.FundingDeadline,
            ExpectedRevenue=entity.ExpectedRevenue,
            ExpectedCost=entity.ExpectedCost,
            ExpectedProfit=entity.ExpectedProfit,
            InvestorProfitSharePercentage=entity.InvestorProfitSharePercentage,
            ActualRevenue=entity.ActualRevenue,
            ActualCost=entity.ActualCost,
            ActualProfit=entity.ActualProfit,
            DurationInMonths=entity.DurationInMonths,
            ExpectedStartDate=entity.ExpectedStartDate,
            ExpectedEndDate=entity.ExpectedEndDate,
            Status=entity.Status,
            FarmerId=entity.FarmerId,
            LandId=entity.LandId
        };
    }

    public static InvestmentProjectListItemDto ToDto2(this InvestmentProject entity)
    {
        return new InvestmentProjectListItemDto
        {
            Id=entity.Id,
            Title=entity.Title,
            CoverImageUrl=entity.CoverImageUrl,
            RequiredAmount=entity.RequiredAmount,
            AmountCollected=entity.AmountCollected,
            FundingDeadline=entity.FundingDeadline,
            ExpectedProfit=entity.ExpectedProfit,
            Status=entity.Status,
            FarmerId=entity.FarmerId,
            LandId=entity.LandId
        };
    }
    public static List<InvestmentProjectListItemDto> ToDtos(this IEnumerable<InvestmentProject> entities)
    {
        return [..entities.Select(e => e.ToDto2())];
    }
    public static InvestorContributionListItemDto ToDto2(this InvestorContribution entity)
    {
        return new InvestorContributionListItemDto
        {
            Id = entity.Id,
            InvestmentProjectId = entity.InvestmentProjectId,
            Amount = entity.Amount,
            ProfitAmount = entity.ProfitAmount,
            Status = entity.Status,
            ProjectTitle = entity.InvestmentProject!.Title,
            ProjectCoverImageUrl = entity.InvestmentProject.CoverImageUrl,
            ProjectStatus = entity.InvestmentProject.Status,
            InvestorId=entity.InvestorId,
            CreatedAt=entity.CreatedAtUtc
        };
    }
    public static List<InvestorContributionListItemDto> ToDtos(this IEnumerable<InvestorContribution> entities)
    {
        return [..entities.Select(e => e.ToDto2())];
    }
}