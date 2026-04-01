namespace Namaa.Application.Features.Recommendations.Dtos;
public class CropDetailsDto
{
    public int Id { get; init; } 
    public string Name { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Season { get; init; } = string.Empty;
    public string PlantingTime { get; init; } = string.Empty;
    public string HarvestTime { get; init; } = string.Empty;
    public int DaysToHarvest { get; init; }
}