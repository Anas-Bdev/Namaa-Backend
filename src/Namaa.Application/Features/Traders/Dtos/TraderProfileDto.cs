
namespace Namaa.Application.Features.Traders.Dtos;
public class TraderProfileDto
{
    public Guid Id {get;set;}
    public string BusinessName {get;set;}=string.Empty;
    public string BusinessType {get;set;}=string.Empty;
    public int GovernorateId {get;set;}
    public string Governorate {get;set;}=string.Empty;
    public string? AddressDetail { get; set; }

}