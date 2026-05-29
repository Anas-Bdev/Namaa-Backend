using Namaa.Application.Features.Lands.Dtos;
using Namaa.Domain.ReferenceData;

namespace Namaa.Application.Features.Lands.Mappers;
public static class CropMapper{

    public static CropDto ToDto(this Crop entity)
    {
        return new CropDto
        {
            CropId=entity.Id,
            CropName=entity.Name!
        };
    }
    public static List<CropDto> ToDtos(this IEnumerable<Crop> entities)
    {
        return [..entities.Select(e => e.ToDto())];
    }
}