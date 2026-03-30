namespace Namaa.Application.Features.Investors.Dtos;

public class InvestorProfileDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? OrganizationName { get; set; }
    public string? CompanyName { get; set; }
    public int CityId { get; set; }
    public string? AddressDetail { get; set; }
}