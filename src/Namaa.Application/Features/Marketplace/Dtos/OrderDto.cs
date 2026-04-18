namespace Namaa.Application.Features.Marketplace.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid TraderId { get; set; }
    public string TraderName { get; set; } = string.Empty;
    public Guid ListingId { get; set; }
    public string ListingTitle { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? PurchasedAt { get; set; }
}