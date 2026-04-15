using System.ComponentModel.DataAnnotations;
using Namaa.Domain.Enums;

namespace Namaa.Api.Contracts.Requests.Traders;
public class UpdateTraderProfileRequest
{
    
    [Required(ErrorMessage = "City ID is required.")]
    public int? CityId { get; init; }
    public string? AddressDetail { get; init; }

    [Required(ErrorMessage = "Business Type is required.")]
    public TraderType? TraderType {get;init;}

    [Required(ErrorMessage = "Business Name is required.")]
    public string BusinessName {get;init;}=default!;
}