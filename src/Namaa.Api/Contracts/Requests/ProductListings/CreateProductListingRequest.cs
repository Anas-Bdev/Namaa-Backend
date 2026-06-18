using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.ProductListings;
public class CreateProductListingRequest
{
    [Required]
    public string CropName {get;set;}=default!;

    [Required]
    public  string Category {get;set;}=default!;
    public Guid? SeedingCycleId { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [StringLength(20)]
    public string Unit { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal PricePerUnit { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal? DiscountPrice { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal QuantityAvailable { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? HarvestDate { get; set; }
}