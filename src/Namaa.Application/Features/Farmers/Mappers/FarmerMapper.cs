using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Domain.Profiles.Farmer;

namespace Namaa.Application.Features.Farmers.Mappers;

public static class FarmerMapper
{
    public static FarmerProfileDto ToDto(this FarmerProfile farmer, string fullName)
    {
        return new FarmerProfileDto
        {
            Id = farmer.Id,
            FullName = fullName,
            Description = farmer.Description,
            CityId = farmer.CityId ?? 0,
            AddressDetail = farmer.AddressDetail,
            ExperienceLevel = farmer.ExperienceLevel
        };
    }

    public static FarmerSummaryDto ToSummaryDto(this FarmerProfile farmer, string fullName)
    {
        return new FarmerSummaryDto
        {
            Id = farmer.Id,
            FullName = fullName,
            CityId = farmer.CityId ?? 0,
            ExperienceLevel = farmer.ExperienceLevel
        };
    }
}