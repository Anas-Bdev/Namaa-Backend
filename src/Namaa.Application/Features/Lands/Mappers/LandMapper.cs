using Namaa.Application.Features.Lands.Dtos;
using Namaa.Domain.Land;

namespace Namaa.Application.Features.Lands.Mappers;
public static class LandMapper
{
    public static LandDto ToDto(this Land entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        return new LandDto
        {
            LandId=entity.Id,
            Name=entity.Name!,
            AreaDonum=entity.Area,
            CityId=entity.CityId,
            SoilId=entity.SoilId,
            WaterSourceType=entity.WaterSourceType,
            WaterAvailability=entity.WaterAvailability,
            EnvironmentType=entity.EnvironmentType
        };
    }
    public static List<LandDto> ToDtos(this IEnumerable<Land> entities)
    {
        return [.. entities.Select(e => e.ToDto())];
    }
    
}