using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Marketplace;

public static class FarmerRatingErrors
{
    public static readonly Error IdRequired = Error.Validation(
        "FarmerRating.IdRequired", "A valid ID must be provided.");
    public static readonly Error InvalidRatingValue = Error.Validation(
        "FarmerRating.InvalidValue", "Rating value must be between 1 and 5.");
    public static readonly Error AlreadyRated = Error.Conflict(
        "FarmerRating.AlreadyRated", "This order has already been rated.");
}
