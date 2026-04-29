namespace Namaa.Application.Features.Marketplace.Dtos;

public class ProductListingSummaryDto
{
    public Guid Id { get; set; }
    public string FarmerName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Unit { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal? DiscountPrice { get; set; }
    public double QuantityAvailable { get; set; }
    public string? ImageUrl { get; set; }
}