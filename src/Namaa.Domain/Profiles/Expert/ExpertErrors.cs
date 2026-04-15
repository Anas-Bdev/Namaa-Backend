using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Profiles.Expert;

public static class ExpertErrors
{

    public static readonly Error UserIdRequired = Error.Validation(
        "Expert.UserIdRequired", "A valid User ID must be provided.");
        
    public static readonly Error CvRequired = Error.Validation(
        "Expert.CvRequired", "The CV URL is required for verification.");


    public static readonly Error InvalidExperience = Error.Validation(
        "Expert.InvalidExperience", "Years of experience cannot be negative.");

    public static readonly Error CityRequired = Error.Validation(
        "Expert.CityRequired", "A valid city must be selected.");

    public static readonly Error AvailabilityIdRequired = Error.Validation(
        "Expert.AvailabilityIdRequired", "Availability ID is required.");

    public static readonly Error InvalidTimeRange = Error.Validation(
        "Expert.InvalidTimeRange", "Start time must be before end time.");
     public static readonly Error CvUrlRequired = Error.Validation(
            "Expert.CvUrlRequired", "The CV URL is required and cannot be empty.");   

  
}