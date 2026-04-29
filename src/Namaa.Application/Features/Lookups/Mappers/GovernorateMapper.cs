using Namaa.Domain.ReferenceData;

namespace Namaa.Application.Features.Lookups.Mappers;
public static class GovernorateMapper
{
    public static GovernorateDto ToDto(this Governorate entity)
    {
        return new GovernorateDto{
            Id=entity.Id,
            Name=entity.Name!
        };
    }
    public static List<GovernorateDto> ToDtos(this IEnumerable<Governorate> entities)
    {
        return [..entities.Select(e => e.ToDto())];
    }
} 