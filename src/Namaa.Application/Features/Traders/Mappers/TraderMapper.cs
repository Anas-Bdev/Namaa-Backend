using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Profiles.Trader;

namespace Namaa.Application.Features.Traders.Mappers;
public static class TraderMapper
{
    public static TraderProfileDto ToDto(this TraderProfile entity)
    {
        return new TraderProfileDto
        {
         Id=entity.Id,
         BusinessName=entity.BusinessName!,
         BusinessType=entity.BusinessType.ToString(),
         GovernorateId=entity.GovernorateId,
         Governorate=entity.Governorate!.Name!,
         AddressDetail=entity.AddressDetail
        };
    }
}