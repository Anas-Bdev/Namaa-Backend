namespace Namaa.Application.Features.Farmers.Dtos;
public class FarmerProfileDto
{
    public Guid Id { get; set; }
    public string? Description {get;set;}
    public int GovernorateId { get; set; }
    public string Governorate {get;set;}=string.Empty;
    public string? AddressDetail { get; set; }=string.Empty;
}