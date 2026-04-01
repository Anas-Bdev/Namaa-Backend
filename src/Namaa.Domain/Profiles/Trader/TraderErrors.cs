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
    
}
