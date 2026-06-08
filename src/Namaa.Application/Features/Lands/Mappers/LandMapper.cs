using Namaa.Application.Features.Lands.Dtos;
using Namaa.Domain.Lands;

namespace Namaa.Application.Features.Lands.Mappers;
public static class LandMapper
{
    public static LandDto ToDto(this Land entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        return new LandDto
        {
            GovernorateName=entity.Governorate!.Name!,
            SoilTypeName=entity.SoilType!.Name!,
            LandId=entity.Id,
            Name=entity.Name!,
            AreaDonum=entity.Area,
            GovernorateId=entity.Governorate!.Id,
            SoilTypeId=entity.SoilType!.Id,
            WaterSourceType=entity.WaterSourceType,
            WaterAvailability=entity.WaterAvailability,
            EnvironmentType=entity.EnvironmentType,
            IrrigationMethod=entity.IrrigationMethod,
            AddressDetail=entity.AddressDetail!

        };
    }
    public static List<LandDto> ToDtos(this IEnumerable<Land> entities)
    {
        return [.. entities.Select(e => e.ToDto())];
    }
    
}