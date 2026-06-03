using System.ComponentModel.Design;
using System.Runtime.Versioning;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.MarketPlace;

namespace Namaa.Application.Features.MarketPlace.Mappers;
public static class MarketPlaceMapper
{
  public static ProductListingDto ToDto(this ProductListing entity,string? cropName=null)
    {
      string finalCropName=cropName ?? entity.Crop?.Name!;
        return new ProductListingDto
        {
         Id=entity.Id,
         FarmerId=entity.FarmerId,
         SeedingCycleId=entity.SeedingCycleId,
         CropId=entity.CropId,
         Title=entity.Title,
         Description=entity.Description,
         Unit=entity.Unit,
         PricePerUnit=entity.PricePerUnit,
         DiscountPrice=entity.DiscountPrice,
         QuantityAvailable=entity.QuantityAvailable,
         Status=entity.Status,
         IsAvailable=entity.IsAvailable,
         ImageUrl=entity.ImageUrl,
         HarvestDate=entity.HarvestDate,
         CropName=finalCropName

        };
    }

    public static ProductOrderDto ToDto(this ProductOrder entity)
  {
    return new ProductOrderDto
    {
      OrderId=entity.Id,
      ProductListingId=entity.ProductListingId,
      Quantity=entity.Quantity,
      UnitPriceAtPurchase=entity.PriceAtPurchase,
      TotalPrice=entity.TotalPrice,
      Status=entity.Status,
      DeliveryAddress=entity.DeliveryAddress,
      DeliveryNotes=entity.DeliveryNotes
    };
  }
  
  public static FarmerRatingDto ToDto(this FarmerRating entity,string reviewerName)
  {
    return new FarmerRatingDto
    {
      RatingId=entity.Id,
      OrderId=entity.OrderId,
      ReviewerId=entity.ReviewerTraderId,
      ReviewerName=reviewerName,
      RatingValue=entity.RatingValue,
      Comment=entity.Comment,
      CreatedAt=entity.CreatedAtUtc
    };
  }

  public static List<ProductOrderDto> ToDtos(this IEnumerable<ProductOrder> entities)
  {
    return [..entities.Select(e => e.ToDto())];
  }
  public static List<ProductListingDto> ToDtos(this IEnumerable<ProductListing> entities)
  {
    return [..entities.Select(e => e.ToDto())];
  }

}