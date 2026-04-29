using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Marketplace;

public static class ProductListingErrors
{
    public static readonly Error IdRequired = Error.Validation(
        "ProductListing.IdRequired", "A valid ID must be provided.");
    public static readonly Error TitleRequired = Error.Validation(
        "ProductListing.TitleRequired", "Title is required.");
    public static readonly Error InvalidPrice = Error.Validation(
        "ProductListing.InvalidPrice", "Price must be greater than zero.");
    public static readonly Error InvalidQuantity = Error.Validation(
        "ProductListing.InvalidQuantity", "Quantity must be greater than zero.");
    public static readonly Error InvalidDiscountPrice = Error.Validation(
        "ProductListing.InvalidDiscountPrice", "Discount price must be less than original price.");
}