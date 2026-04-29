namespace Namaa.Application.Features.Investors.Dtos;
public class InvestorListItemDto
{
    public Guid Id {get;set;}
    public string FullName {get;set;}=string.Empty;
    public string? OrganizationName {get;set;}
    public string InvestorType {get;set;}=string.Empty;
    public string Governorate {get;set;}=string.Empty;
    public string?  ProfileImageUrl {get;set;}
}