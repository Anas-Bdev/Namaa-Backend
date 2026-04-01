namespace Namaa.Api.Contracts.Requests.Investment;

public record CreateContributionRequest(
    Guid ProjectId,
    decimal Amount
);