using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.Farmers;
public class CreateFarmerProfileRequest
{
    
    [Required(ErrorMessage = "City ID is required.")]
    public int? CityId { get; init; }
    public string? Description {get;init;}
    public string? AddressDetail { get; init; }
    
}