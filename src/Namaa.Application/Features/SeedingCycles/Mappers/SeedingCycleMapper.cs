using Namaa.Application.Features.SeedingCycles.Dtos;
using Namaa.Domain.Common;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Application.Features.SeedingCycles.Mappers;
public static class SeedingCycleMapper
{
    public static SeedingCycleDto ToDto(this SeedingCycle entity)
    {
        return new SeedingCycleDto
        {
            SeedingCycleId=entity.Id,
            LandId=entity.LandId,
            StartDate=entity.StartDate,
            EstimatedHarvestDate=entity.EstimatedHarvestDate,
            ActualHarvestDate=entity.ActualHarvestDate,
            Status=entity.Status.ToSpacedName(),
            SeedQuantity=entity.SeedQuantityKg,
            SeedingArea=entity.SeedingAreaDunums,
            ExpectedYield=entity.ExpectedYieldKg,
            ActualYield=entity.ActualYieldKg,
            CropName=entity.CropName,
            EnvironmentType=entity.EnvironmentType.ToSpacedName()
        };
    }

    public static List<SeedingCycleDto> ToDtos(this IEnumerable<SeedingCycle> entities)
    {
        return [..entities.Select(e => e.ToDto())];
    }
}