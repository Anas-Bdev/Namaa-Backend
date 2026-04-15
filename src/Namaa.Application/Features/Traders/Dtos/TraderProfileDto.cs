namespace Namaa.Application.Features.Traders.Dtos;

public class TraderProfileDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? BusinessName { get; set; }
    public string? BusinessType { get; set; }
    public string? PreferredCategories { get; set; }
    public int CityId { get; set; }
    public string? AddressDetail { get; set; }
}
