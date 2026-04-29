namespace Namaa.Application.Features.Farmers.Dtos;
public class FarmerListItemDto
{
    public Guid Id {get;set;}
    public string FullName {get;set;}=string.Empty;
    public string? PhoneNumber {get;set;}
    public string? Description {get;set;}
    public string? ProfileImageUrl {get;set;}
    public string Governorate {get;set;}=string.Empty;
}