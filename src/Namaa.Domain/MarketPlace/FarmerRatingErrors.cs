using Namaa.Domain.Common.Results;

namespace Namaa.Domain.MarketPlace;
public static class FarmerRatingErrors
{
    public static readonly Error IdRequired = Error.Validation(
        "FarmerRating.IdRequired", "A valid Rating ID is required.");
        
    public static readonly Error OrderIdRequired = Error.Validation(
        "FarmerRating.OrderIdRequired", "An order ID is required to verify the purchase.");
        
    public static readonly Error ReviewerTraderIdRequired = Error.Validation(
        "FarmerRating.ReviewerTraderIdRequired", "The reviewer (Trader) ID is required.");
        
    public static readonly Error FarmerIdRequired = Error.Validation(
        "FarmerRating.FarmerIdRequired", "The target Farmer ID is required.");

    public static readonly Error InvalidRatingValue = Error.Validation(
        "FarmerRating.InvalidRatingValue", "Rating value must be strictly between 1 and 5.");
}