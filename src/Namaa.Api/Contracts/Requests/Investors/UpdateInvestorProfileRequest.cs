using System.ComponentModel.DataAnnotations;
using Namaa.Domain.Enums;

namespace Namaa.Api.Contracts.Requests.Investors;
public class UpdateInvestorProfileRequest
{
    [Required(ErrorMessage = "City ID is required.")]
    public int? CityId { get; init; }
    public string? AddressDetail { get; init; }

    [Required(ErrorMessage ="Investor Type is required.")]
    public InvestorType? InvestorType {get;init;}

    public string? OrganizationName {get;init;}

}