namespace Namaa.Application.Features.Farmers.Dtos;

public class FarmerProfileDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CityId { get; set; }
    public string? AddressDetail { get; set; }
    public string? ExperienceLevel { get; set; }
}