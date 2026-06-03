using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.ProductOrders;
public class CreateProductOrderRequest
{
    [Required]
    public Guid ProductListingId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public decimal Quantity { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Governorate { get; set; } = default!;

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string CityOrVillage { get; set; } =default!;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string NeighborhoodOrStreet { get; set; } = default!;

    public string? LandMark { get; set; }

    public string? DeliveryNotes { get; set; }
}