using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Trader;
public static class TraderErrors
{
    public static readonly Error UserIdRequired = Error.Validation(
        "Trader.UserIdRequired", "A valid User ID must be provided.");

    public static readonly Error CityRequired = Error.Validation(
        "Trader.CityRequired", "A valid city (Governorate) must be selected.");

    public static readonly Error BusinessNameRequired = Error.Validation(
        "Trader.BusinessNameRequired", "The trader's business name is required.");

    public static readonly Error InvalidTraderType = Error.Validation(
        "Trader.InvalidType", "The provided trader type is not valid.");
}