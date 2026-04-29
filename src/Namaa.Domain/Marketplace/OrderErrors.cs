using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Marketplace;

public static class OrderErrors
{
    public static readonly Error IdRequired = Error.Validation(
        "Order.IdRequired", "A valid ID must be provided.");
    public static readonly Error InvalidQuantity = Error.Validation(
        "Order.InvalidQuantity", "Quantity must be greater than zero.");
    public static readonly Error InsufficientQuantity = Error.Conflict(
        "Order.InsufficientQuantity", "Requested quantity exceeds available stock.");
    public static readonly Error InvalidStatusTransition = Error.Conflict(
        "Order.InvalidStatus", "Invalid status transition.");
    public static readonly Error CannotCancelPaidOrder = Error.Conflict(
        "Order.CannotCancel", "Cannot cancel a paid order.");
}