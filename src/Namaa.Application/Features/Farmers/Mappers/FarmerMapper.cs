using Microsoft.EntityFrameworkCore.Storage.Json;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Domain.Profiles.Farmer;

namespace Namaa.Application.Features.Farmers.Mappers;
public static class FarmerMapper
{
    public static FarmerProfileDto ToDto(this FarmerProfile entity)
    {
        return new FarmerProfileDto
        {
            Id=entity.Id,
            Description=entity.Description,
            GovernorateId=entity.GovernorateId,
            Governorate=entity.Governorate!.Name!,
            AddressDetail=entity.AddressDetail
        };
    }

}