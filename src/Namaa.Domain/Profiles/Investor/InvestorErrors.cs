using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Investor;

public static class InvestorErrors
{
    public static readonly Error UserIdRequired = Error.Validation(
        "Investor.UserIdRequired", "A valid User ID must be provided.");
    public static readonly Error OrganizationNameRequired = Error.Validation(
        "Investor.OrganizationNameRequired", "Organization name is required.");
    public static readonly Error CityRequired = Error.Validation(
        "Investor.CityRequired", "A valid city must be selected.");
    public static readonly Error InvestorAlreadyExists = Error.Conflict(
    "Investor.AlreadyExists", "An investor profile already exists for this user.");
    public static readonly Error InvestorNotFound = Error.NotFound(
        "Investor.NotFound", "Investor profile not found.");

}
