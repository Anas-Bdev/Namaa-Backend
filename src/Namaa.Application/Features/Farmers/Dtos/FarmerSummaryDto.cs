namespace Namaa.Application.Features.Farmers.Dtos;

public class FarmerSummaryDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int CityId { get; set; }
    public string? ExperienceLevel { get; set; }
}