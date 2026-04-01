namespace Namaa.Api.Contracts.Requests.Investment;

public record UpdateInvestmentProjectRequest(
    string Title,
    string? Description,
    decimal RequiredAmount,
    decimal? ExpectedProfit,
    decimal? SharePercentage
);