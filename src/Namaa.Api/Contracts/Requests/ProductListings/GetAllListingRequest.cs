using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.ProductListings;
public class GetAllListingsRequest
{
    public string? Category { get; set; }
    public string? Location { get; set; }
    
    [Range(0, double.MaxValue)]
    public decimal? MinPrice { get; set; }
    
    [Range(0, double.MaxValue)]
    public decimal? MaxPrice { get; set; }
    
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 5;
    
    [Range(1, 100)] // Limit page size to 100 to prevent database exhaustion
    public int PageSize { get; set; } = 10;
}