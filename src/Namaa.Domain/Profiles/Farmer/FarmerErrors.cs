using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Farmer;
public static class FarmerErrors
{
    public static readonly Error UserIdRequired = Error.Validation(
        "Farmer.UserIdRequired", "A valid User ID must be provided.");

    public static readonly Error CityRequired = Error.Validation(
        "Farmer.CityRequired", "A valid city (Governorate) must be selected.");

    public static readonly Error InvalidExperienceLevel = Error.Validation(
        "Farmer.InvalidExperienceLevel", "The provided experience level is not valid.");
}