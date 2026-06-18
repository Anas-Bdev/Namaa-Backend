using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.ReferenceData;

namespace Namaa.Domain.MarketPlace;
public sealed class ProductListing : AuditableEntity
{
    public string CropName { get; private set; }
    public string Category { get; private set; }
    public Guid FarmerId { get; private set; }
    public Guid? SeedingCycleId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime? HarvestDate { get; private set; }
    public string? ImageUrl { get; private set; }
    public string Unit { get; private set; }
    public decimal PricePerUnit { get; private set; }
    public decimal? DiscountPrice { get; private set; }
    public decimal QuantityAvailable { get; private set; }
    public ListingStatus Status { get; private set; }
    public bool IsAvailable => QuantityAvailable > 0 && Status == ListingStatus.Active;

    #pragma warning disable CS8618
    private ProductListing() { }
    #pragma warning restore CS8618

    private ProductListing(
        Guid id,
        Guid farmerId,
        Guid? seedingCycleId,
        string cropName,
        string category,
        string title,
        string? description,
        string unit,
        decimal pricePerUnit,
        decimal? discountPrice,
        decimal quantityAvailable,
        string? imageUrl,
        DateTime? harvestDate) : base(id)
    {
        FarmerId = farmerId;
        SeedingCycleId = seedingCycleId;
        CropName = cropName;
        Category = category;
        Title = title;
        Description = description;
        Unit = unit;
        PricePerUnit = pricePerUnit;
        DiscountPrice = discountPrice;
        QuantityAvailable = quantityAvailable;
        ImageUrl = imageUrl;
        HarvestDate = harvestDate;
        Status = ListingStatus.Active;
    }

    public static Result<ProductListing> Create(
        Guid id,
        Guid farmerId,
        Guid? seedingCycleId,
        string cropName,
        string category,
        string title,
        string? description,
        string unit,
        decimal pricePerUnit,
        decimal? discountPrice,
        decimal quantityAvailable,
        string? imageUrl,
        DateTime? harvestDate)
    {
        if (id == Guid.Empty) return ProductListingErrors.IdRequired;
        if (farmerId == Guid.Empty) return ProductListingErrors.FarmerIdRequired;
        if (string.IsNullOrWhiteSpace(cropName)) return ProductListingErrors.CropNameRequired;
        if (string.IsNullOrWhiteSpace(category)) return ProductListingErrors.CategoryRequired;
        if (string.IsNullOrWhiteSpace(title)) return ProductListingErrors.TitleRequired;
        if (string.IsNullOrWhiteSpace(unit)) return ProductListingErrors.UnitRequired;
        if (pricePerUnit <= 0) return ProductListingErrors.InvalidPrice;
        if (quantityAvailable < 0) return ProductListingErrors.InvalidQuantity;
        if (discountPrice.HasValue && discountPrice >= pricePerUnit) return ProductListingErrors.InvalidDiscountPrice;

        return new ProductListing(id, farmerId, seedingCycleId, cropName.Trim(), category.Trim(), title, description, 
                                  unit, pricePerUnit, discountPrice, quantityAvailable, imageUrl, harvestDate);
    }

    public Result<Updated> Update(
        string title,
        string? description,
        string cropName,
        string category,
        string unit,
        decimal pricePerUnit,
        decimal? discountPrice,
        decimal quantityAvailable,
        string? imageUrl,
        DateTime? harvestDate)
    {
        if (string.IsNullOrWhiteSpace(title)) return ProductListingErrors.TitleRequired;
        if (string.IsNullOrWhiteSpace(cropName)) return ProductListingErrors.CropNameRequired;
        if (string.IsNullOrWhiteSpace(category)) return ProductListingErrors.CategoryRequired;
        if (string.IsNullOrWhiteSpace(unit)) return ProductListingErrors.UnitRequired;
        if (pricePerUnit <= 0) return ProductListingErrors.InvalidPrice;
        if (quantityAvailable < 0) return ProductListingErrors.InvalidQuantity;
        if (discountPrice.HasValue && discountPrice >= pricePerUnit) return ProductListingErrors.InvalidDiscountPrice;

        Title = title;
        Description = description;
        CropName = cropName.Trim();
        Category = category.Trim();
        Unit = unit;
        PricePerUnit = pricePerUnit;
        DiscountPrice = discountPrice;
        QuantityAvailable = quantityAvailable;
        ImageUrl = imageUrl;
        HarvestDate = harvestDate;

        if (QuantityAvailable > 0 && Status == ListingStatus.SoldOut)
            Status = ListingStatus.Active;

        return Result.Updated;
    }

    public Result<Updated> ReduceQuantity(decimal quantity)
    {
        if (quantity <= 0) return ProductListingErrors.InvalidQuantity;
        if (quantity > QuantityAvailable) return ProductListingErrors.InsufficientStock;

        QuantityAvailable -= quantity;

        if (QuantityAvailable == 0)
            Status = ListingStatus.SoldOut;

        return Result.Updated;
    }

    public Result<Updated> Pause()
    {
        if(Status == ListingStatus.Archived) return ProductListingErrors.AlreadyArchived;
        if(Status == ListingStatus.SoldOut) return ProductListingErrors.AlreadySoldOut;
        Status = ListingStatus.Paused;
        return Result.Updated;
    }

    public Result<Updated> Resume()
    {
        if (Status == ListingStatus.Archived) return ProductListingErrors.AlreadyArchived;
        if (QuantityAvailable <= 0) return ProductListingErrors.InvalidQuantity; 

        Status = ListingStatus.Active;
        return Result.Updated;
    }

    public Result<Updated> Archive()
    {
        Status = ListingStatus.Archived;
        return Result.Updated;
    }

    public Result<Updated> IncreaseQuantity(decimal quantity)
    {
        if (quantity <= 0) return ProductListingErrors.InvalidQuantity;

        QuantityAvailable += quantity;

        if (Status == ListingStatus.SoldOut)
            Status = ListingStatus.Active;

        return Result.Updated;
    }
}