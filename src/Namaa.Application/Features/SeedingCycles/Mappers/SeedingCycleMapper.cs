using Namaa.Application.Features.SeedingCycles.Dtos;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Application.Features.SeedingCycles.Mappers;
public static class SeedingCycleMapper
{
    public static SeedingCycleDto ToDto(this SeedingCycle entity)
    {
        return new SeedingCycleDto
        {
            SeedingCycleId=entity.Id,
            CropId=entity.CropId,
            LandId=entity.LandId,
            StartDate=entity.StartDate,
            EstimatedHarvestDate=entity.EstimatedHarvestDate,
            ActualHarvestDate=entity.ActualHarvestDate,
            Status=entity.Status,
            SeedQuantity=entity.SeedQuantity,
            SeedingArea=entity.SeedingArea,
            ExpectedYield=entity.ExpectedYield,
            ActualYield=entity.ActualYield,
            CropName=entity.Crop!.Name!,
            EnvironmentType=entity.EnvironmentType
        };
    }

    public static List<SeedingCycleDto> ToDtos(this IEnumerable<SeedingCycle> entities)
    {
        return [..entities.Select(e => e.ToDto())];
    }
}