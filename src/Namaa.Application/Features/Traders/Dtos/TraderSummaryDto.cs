namespace Namaa.Application.Features.Traders.Dtos;

public class TraderSummaryDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? BusinessName { get; set; }
    public string? BusinessType { get; set; }
    public int CityId { get; set; }
}
