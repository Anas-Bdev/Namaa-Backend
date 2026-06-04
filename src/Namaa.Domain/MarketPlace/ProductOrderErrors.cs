using Namaa.Domain.Common.Results;

namespace Namaa.Domain.MarketPlace;
public static class ProductOrderErrors
{
    public static readonly Error IdRequired = Error.Validation(
        "ProductOrder.IdRequired", "A valid Order ID is required.");
    public static readonly Error TraderIdRequired = Error.Validation(
        "ProductOrder.TraderIdRequired", "A valid Trader ID is required.");
    public static readonly Error ProductListingIdRequired = Error.Validation(
        "ProductOrder.ProductListingIdRequired", "A valid Product Listing ID is required.");
        
    public static readonly Error InvalidQuantity = Error.Validation(
        "ProductOrder.InvalidQuantity", "Quantity must be greater than zero.");
    public static readonly Error InvalidPrice = Error.Validation(
        "ProductOrder.InvalidPrice", "Purchase price must be greater than zero.");
        
    public static readonly Error InvalidStatusTransition = Error.Conflict(
        "ProductOrder.InvalidStatusTransition", "This action cannot be performed in the current order state.");
    public static readonly Error CannotCancelPaidOrder = Error.Conflict(
        "ProductOrder.CannotCancelPaidOrder", "Cannot cancel an order that has already been paid for.");
    public static readonly Error AddressRequired = Error.Validation("ProductOrder.AddressRequired", "A delivery address is required.");
    public static readonly Error InvalidArrivalDate= Error.Validation("ProductOrder.InvalidArrivalDate","The estimated arrival date must be in the future");
}