namespace Namaa.Application.Features.Traders.Dtos;
public class TraderListItemDto
{
    public Guid Id {get;set;}
    public string FullName {get;set;}=string.Empty;
    public string Governorate {get;set;}=string.Empty;
    public string BusinessName {get;set;}=string.Empty;
    public string BusinessType {get;set;}=string.Empty;
    public string? ProfileImageUrl {get;set;}
}