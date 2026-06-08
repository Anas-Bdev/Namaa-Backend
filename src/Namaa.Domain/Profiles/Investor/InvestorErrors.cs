using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Investor;
public static class InvestorErrors
{
    public static readonly Error UserIdRequired = Error.Validation(
        "Investor.UserIdRequired", "A valid User ID must be provided.");

    public static readonly Error CityRequired = Error.Validation(
        "Investor.CityRequired", "A valid city (Governorate) must be selected.");

    public static readonly Error InvalidInvestorType = Error.Validation(
        "Investor.InvalidType", "The provided investor type is not valid.");

    public static readonly Error OrganizationNameRequired = Error.Validation(
        "Investor.OrganizationNameRequired", "An organization name is required for companies and government entities.");
}