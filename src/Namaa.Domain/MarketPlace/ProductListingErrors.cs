using Namaa.Domain.Common.Results;

namespace Namaa.Domain.MarketPlace;
public static class ProductListingErrors
{
    public static readonly Error IdRequired = Error.Validation(
        "ProductListing.IdRequired", "A valid Product Listing ID must be provided.");
        
    public static readonly Error FarmerIdRequired = Error.Validation(
        "ProductListing.FarmerIdRequired", "A valid Farmer ID is required to create a listing.");
        
    public static readonly Error CropIdRequired = Error.Validation(
        "ProductListing.CropIdRequired", "A valid Crop must be selected.");

    // --- 2. String/Detail Validation Errors ---
    public static readonly Error TitleRequired = Error.Validation(
        "ProductListing.TitleRequired", "The product title cannot be empty.");
        
    public static readonly Error UnitRequired = Error.Validation(
        "ProductListing.UnitRequired", "A unit of measurement (e.g., KG, Box, Liter) must be specified.");

    // --- 3. Commerce Validation Errors ---
    public static readonly Error InvalidPrice = Error.Validation(
        "ProductListing.InvalidPrice", "The price per unit must be greater than zero.");
        
    public static readonly Error InvalidQuantity = Error.Validation(
        "ProductListing.InvalidQuantity", "The quantity cannot be negative.");
        
    public static readonly Error InvalidDiscountPrice = Error.Validation(
        "ProductListing.InvalidDiscountPrice", "The discount price must be strictly less than the original price.");

    // --- 4. State / Business Rule Errors ---
    public static readonly Error InsufficientStock = Error.Conflict(
        "ProductListing.InsufficientStock", "There is not enough stock available to fulfill this request.");
        
    public static readonly Error AlreadyArchived = Error.Conflict(
        "ProductListing.AlreadyArchived", "This product listing has been archived and cannot be modified.");
    public static readonly Error AlreadySoldOut = Error.Conflict(
        "ProductListing.AlreadySoldOut", "This product listing has been SoldOut and cannot be paused.");

    public static readonly Error CropNameRequired = Error.Validation(
        "ProductListing.CropNameRequired", "The crop name cannot be empty.");

    public static readonly Error CategoryRequired = Error.Validation(
        "ProductListing.CategoryRequired", "The category cannot be empty.");
}