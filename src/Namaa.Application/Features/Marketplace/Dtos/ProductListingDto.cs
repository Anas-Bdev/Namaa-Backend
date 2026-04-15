namespace Namaa.Application.Features.Marketplace.Dtos;

public class ProductListingDto
{
    public Guid Id { get; set; }
    public Guid SeedingCycleId { get; set; }
    public string FarmerName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Unit { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal? DiscountPrice { get; set; }
    public double QuantityAvailable { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime? HarvestDate { get; set; }
}