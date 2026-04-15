using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Investment;

public static class InvestorContributionErrors
{
    public static readonly Error ContributionIdRequired = Error.Validation(
        "Contribution.IdRequired", "A valid Contribution ID must be provided.");
    public static readonly Error InvalidAmount = Error.Validation(
        "Contribution.InvalidAmount", "Contribution amount must be greater than zero.");
    public static readonly Error Unauthorized = Error.Forbidden(
        "Contribution.Unauthorized", "You are not authorized to respond to this contribution.");
    public static readonly Error AlreadyResponded = Error.Conflict(
        "Contribution.AlreadyResponded", "This contribution has already been responded to.");
}