using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Marketplace;

public sealed class ProductListing : AuditableEntity
{
    public Guid SeedingCycleId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? Unit { get; private set; }
    public decimal PricePerUnit { get; private set; }
    public decimal? DiscountPrice { get; private set; }
    public double QuantityAvailable { get; private set; }
    public string? ImageUrl { get; private set; }
    public DateTime? HarvestDate { get; private set; }

    private readonly List<Order> _orders = [];
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

    private ProductListing() { }

    private ProductListing(
        Guid id,
        Guid seedingCycleId,
        string title,
        string? description,
        string? unit,
        decimal pricePerUnit,
        decimal? discountPrice,
        double quantityAvailable,
        string? imageUrl,
        DateTime? harvestDate) : base(id)
    {
        SeedingCycleId = seedingCycleId;
        Title = title;
        Description = description;
        Unit = unit;
        PricePerUnit = pricePerUnit;
        DiscountPrice = discountPrice;
        QuantityAvailable = quantityAvailable;
        ImageUrl = imageUrl;
        HarvestDate = harvestDate;
    }

    public static Result<ProductListing> Create(
        Guid id,
        Guid seedingCycleId,
        string title,
        string? description,
        string? unit,
        decimal pricePerUnit,
        decimal? discountPrice,
        double quantityAvailable,
        string? imageUrl,
        DateTime? harvestDate)
    {
        if (id == Guid.Empty)
            return ProductListingErrors.IdRequired;
        if (string.IsNullOrWhiteSpace(title))
            return ProductListingErrors.TitleRequired;
        if (pricePerUnit <= 0)
            return ProductListingErrors.InvalidPrice;
        if (quantityAvailable <= 0)
            return ProductListingErrors.InvalidQuantity;
        if (discountPrice.HasValue && discountPrice >= pricePerUnit)
            return ProductListingErrors.InvalidDiscountPrice;

        return new ProductListing(
            id, seedingCycleId, title, description,
            unit, pricePerUnit, discountPrice,
            quantityAvailable, imageUrl, harvestDate);
    }

    public Result<Updated> Update(
        string title,
        string? description,
        string? unit,
        decimal pricePerUnit,
        decimal? discountPrice,
        double quantityAvailable,
        string? imageUrl,
        DateTime? harvestDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            return ProductListingErrors.TitleRequired;
        if (pricePerUnit <= 0)
            return ProductListingErrors.InvalidPrice;
        if (quantityAvailable <= 0)
            return ProductListingErrors.InvalidQuantity;
        if (discountPrice.HasValue && discountPrice >= pricePerUnit)
            return ProductListingErrors.InvalidDiscountPrice;

        Title = title;
        Description = description;
        Unit = unit;
        PricePerUnit = pricePerUnit;
        DiscountPrice = discountPrice;
        QuantityAvailable = quantityAvailable;
        ImageUrl = imageUrl;
        HarvestDate = harvestDate;

        return Result.Updated;
    }

    public Result<Updated> ReduceQuantity(double quantity)
    {
        if (quantity > QuantityAvailable)
            return ProductListingErrors.InvalidQuantity;
        QuantityAvailable -= quantity;
        return Result.Updated;
    }
}