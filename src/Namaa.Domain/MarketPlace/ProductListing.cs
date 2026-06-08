using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.ReferenceData;

namespace Namaa.Domain.MarketPlace;
public sealed class ProductListing : AuditableEntity
{
    public Guid FarmerId { get; private set; }
    public Guid? SeedingCycleId { get; private set; }
    public int CropId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime? HarvestDate { get; private set; }
    public string? ImageUrl { get; private set; }
    public string Unit { get; private set; }
    public decimal PricePerUnit { get; private set; }
    public decimal? DiscountPrice { get; private set; }
    public decimal QuantityAvailable { get; private set; }
    public ListingStatus Status { get; private set; }
    public Crop? Crop {get;set;}

    // Computed property to make UI queries easier
    public bool IsAvailable => QuantityAvailable > 0 && Status == ListingStatus.Active;
    #pragma warning disable CS8618
    private ProductListing() { }
    #pragma warning restore CS8618

    private ProductListing(
        Guid id,
        Guid farmerId,
        Guid? seedingCycleId,
        int cropId,
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
        CropId = cropId;
        Title = title;
        Description = description;
        Unit = unit;
        PricePerUnit = pricePerUnit;
        DiscountPrice = discountPrice;
        QuantityAvailable = quantityAvailable;
        ImageUrl = imageUrl;
        HarvestDate = harvestDate;
        
        // Force status to Active on creation
        Status = ListingStatus.Active;
    }

    public static Result<ProductListing> Create(
        Guid id,
        Guid farmerId,
        Guid? seedingCycleId,
        int cropId,
        string title,
        string? description,
        string unit, // Made strictly non-nullable
        decimal pricePerUnit,
        decimal? discountPrice,
        decimal quantityAvailable, // Fixed from double to decimal
        string? imageUrl,
        DateTime? harvestDate)
    {
        // 1. Validate Keys
        if (id == Guid.Empty)
            return ProductListingErrors.IdRequired;
        if (farmerId == Guid.Empty)
            return ProductListingErrors.FarmerIdRequired;
        if (cropId <= 0)
            return ProductListingErrors.CropIdRequired;

        // 2. Validate Strings
        if (string.IsNullOrWhiteSpace(title))
            return ProductListingErrors.TitleRequired;
        if (string.IsNullOrWhiteSpace(unit))
            return ProductListingErrors.UnitRequired;

        // 3. Validate Business Rules
        if (pricePerUnit <= 0)
            return ProductListingErrors.InvalidPrice;
        if (quantityAvailable < 0) // Changed to < 0 to allow creating empty listings if needed, or keep <= 0 based on your preference
            return ProductListingErrors.InvalidQuantity;
        if (discountPrice.HasValue && discountPrice >= pricePerUnit)
            return ProductListingErrors.InvalidDiscountPrice;

        return new ProductListing(
            id, farmerId, seedingCycleId, cropId, title, description,
            unit, pricePerUnit, discountPrice,
            quantityAvailable, imageUrl, harvestDate);
    }

    public Result<Updated> Update(
        string title,
        string? description,
        int cropId, // Added so they can update the crop if they made a mistake
        string unit, // Made non-nullable
        decimal pricePerUnit,
        decimal? discountPrice,
        decimal quantityAvailable, // Fixed from double to decimal
        string? imageUrl,
        DateTime? harvestDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            return ProductListingErrors.TitleRequired;
        if (string.IsNullOrWhiteSpace(unit))
            return ProductListingErrors.UnitRequired;
        if (cropId <= 0)
            return ProductListingErrors.CropIdRequired;
        if (pricePerUnit <= 0)
            return ProductListingErrors.InvalidPrice;
        if (quantityAvailable < 0)
            return ProductListingErrors.InvalidQuantity;
        if (discountPrice.HasValue && discountPrice >= pricePerUnit)
            return ProductListingErrors.InvalidDiscountPrice;

        Title = title;
        Description = description;
        CropId = cropId;
        Unit = unit;
        PricePerUnit = pricePerUnit;
        DiscountPrice = discountPrice;
        QuantityAvailable = quantityAvailable;
        ImageUrl = imageUrl;
        HarvestDate = harvestDate;

        // Automatically reactivate the listing if they update the quantity from 0 back to a positive number
        if (QuantityAvailable > 0 && Status == ListingStatus.SoldOut)
        {
            Status = ListingStatus.Active;
        }

        return Result.Updated;
    }

    public Result<Updated> ReduceQuantity(decimal quantity) // Fixed from double to decimal
    {
        if (quantity <= 0)
            return ProductListingErrors.InvalidQuantity; // Can't reduce by a negative or zero number
            
        if (quantity > QuantityAvailable)
            return ProductListingErrors.InsufficientStock; // You may need to add this error to your Errors class

        QuantityAvailable -= quantity;

        // Automatically mark as sold out if it hits exactly 0
        if (QuantityAvailable == 0)
        {
            Status = ListingStatus.SoldOut;
        }

        return Result.Updated;
    }

    public Result<Updated> Pause()
    {
        if(Status==ListingStatus.Archived)
        return ProductListingErrors.AlreadyArchived;
        if(Status==ListingStatus.SoldOut)
        return ProductListingErrors.AlreadySoldOut;
        Status=ListingStatus.Paused;
        return Result.Updated;
    }

    public Result<Updated> Resume()
{
    if (Status == ListingStatus.Archived)
        return ProductListingErrors.AlreadyArchived;

    if (QuantityAvailable <= 0)
        return ProductListingErrors.InvalidQuantity; 

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
    if (quantity <= 0)
        return ProductListingErrors.InvalidQuantity;

    QuantityAvailable += quantity;

    // If it was sold out, it's back in stock!
    if (Status == ListingStatus.SoldOut)
    {
        Status = ListingStatus.Active;
    }

    return Result.Updated;
}
}