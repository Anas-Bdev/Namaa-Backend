namespace Namaa.Api.Contracts.Requests.Investment;

public record SetActualResultsRequest(
    decimal ActualRevenue,
    decimal ActualCost
);