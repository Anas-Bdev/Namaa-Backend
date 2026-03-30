using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Trader;

public static class TraderErrors
{
    public static readonly Error UserIdRequired = Error.Validation(
        "Trader.UserIdRequired", "A valid User ID must be provided.");
    public static readonly Error BusinessNameRequired = Error.Validation(
        "Trader.BusinessNameRequired", "Business name is required.");
    public static readonly Error BusinessTypeRequired = Error.Validation(
        "Trader.BusinessTypeRequired", "Business type is required.");
    public static readonly Error CityRequired = Error.Validation(
        "Trader.CityRequired", "A valid city must be selected.");
    public static readonly Error TraderNotFound = Error.NotFound(
      "Trader.NotFound", "Trader profile not found.");

    public static readonly Error TraderAlreadyExists = Error.Conflict(
    "Trader.AlreadyExists", "A trader profile already exists for this user.");

}
