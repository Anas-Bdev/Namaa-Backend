using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Domain.Marketplace;

namespace Namaa.Application.Features.Marketplace.Mappers;

public static class MarketplaceMapper
{
    public static ProductListingDto ToDto(this ProductListing listing, string farmerName)
    {
        return new ProductListingDto
        {
            Id = listing.Id,
            SeedingCycleId = listing.SeedingCycleId,
            FarmerName = farmerName,
            Title = listing.Title,
            Description = listing.Description,
            Unit = listing.Unit,
            PricePerUnit = listing.PricePerUnit,
            DiscountPrice = listing.DiscountPrice,
            QuantityAvailable = listing.QuantityAvailable,
            ImageUrl = listing.ImageUrl,
            HarvestDate = listing.HarvestDate
        };
    }

    public static ProductListingSummaryDto ToSummaryDto(this ProductListing listing, string farmerName)
    {
        return new ProductListingSummaryDto
        {
            Id = listing.Id,
            FarmerName = farmerName,
            Title = listing.Title,
            Unit = listing.Unit,
            PricePerUnit = listing.PricePerUnit,
            DiscountPrice = listing.DiscountPrice,
            QuantityAvailable = listing.QuantityAvailable,
            ImageUrl = listing.ImageUrl
        };
    }

    public static OrderDto ToDto(this Order order, string traderName, string listingTitle)
    {
        return new OrderDto
        {
            Id = order.Id,
            TraderId = order.TraderId,
            TraderName = traderName,
            ListingId = order.ListingId,
            ListingTitle = listingTitle,
            Quantity = order.Quantity,
            PriceAtPurchase = order.PriceAtPurchase,
            TotalPrice = order.PriceAtPurchase * (decimal)order.Quantity,
            Status = order.Status.ToString(),
            PurchasedAt = order.PurchasedAt
        };
    }

    public static FarmerRatingDto ToDto(this FarmerRating rating, string traderName)
    {
        return new FarmerRatingDto
        {
            Id = rating.Id,
            OrderId = rating.OrderId,
            ReviewerTraderName = traderName,
            RatingValue = rating.RatingValue,
            Comment = rating.Comment
        };
    }
}