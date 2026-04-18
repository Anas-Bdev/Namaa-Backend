using Namaa.Application.Features.Lookups.Dtos;
using Namaa.Domain.ReferenceData;

namespace Namaa.Application.Features.Lookups.Mappers;
public static class SoilTypeMapper
{
    public static SoilTypeDto ToDto(this SoilType entity)
    {
        return new SoilTypeDto
        {
            Id=entity.Id,
            Name=entity.Name!
        };
    }
    public static List<SoilTypeDto> ToDtos(this IEnumerable<SoilType> entities)
    {
        return [.. entities.Select(e => e.ToDto())];
    }
}