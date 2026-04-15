using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Profiles.Trader;

namespace Namaa.Application.Features.Traders.Mappers;

public static class TraderMapper
{
    public static TraderProfileDto ToDto(this TraderProfile trader, string fullName)
    {
        return new TraderProfileDto
        {
            Id = trader.Id,
            FullName = fullName,
            BusinessName = trader.BusinessName,
            BusinessType = trader.BusinessType,
            PreferredCategories = trader.PreferredCategories,
            CityId = trader.CityId ?? 0,
            AddressDetail = trader.AddressDetail
        };
    }

    public static TraderSummaryDto ToSummaryDto(this TraderProfile trader, string fullName)
    {
        return new TraderSummaryDto
        {
            Id = trader.Id,
            FullName = fullName,
            BusinessName = trader.BusinessName,
            BusinessType = trader.BusinessType,
            CityId = trader.CityId ?? 0
        };
    }
}
