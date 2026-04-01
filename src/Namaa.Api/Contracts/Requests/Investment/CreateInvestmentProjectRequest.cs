namespace Namaa.Api.Contracts.Requests.Investment;

public record CreateInvestmentProjectRequest(
    string Title,
    string? Description,
    decimal RequiredAmount,
    decimal? ExpectedProfit,
    decimal? SharePercentage
);