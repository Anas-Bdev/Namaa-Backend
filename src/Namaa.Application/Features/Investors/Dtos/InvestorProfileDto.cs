namespace Namaa.Application.Features.Investors.Dtos;
public class InvestorProfileDto
{
    public Guid Id {get;set;}
    public string? OrganizationName {get;set;}
    public string InvestorType {get;set;}=string.Empty;
    public int GovernorateId { get; set; }
    public string Governorate {get;set;}=string.Empty;
    public string? AddressDetail { get; set; }
}