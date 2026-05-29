using Microsoft.EntityFrameworkCore.Storage;
using Namaa.Application.Features.Investors.Dtos;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Profiles.Investor;
using Namaa.Domain.Profiles.Trader;

namespace Namaa.Application.Features.Investors.Mappers;
public static class InvestorMapper
{
    public static InvestorProfileDto ToDto(this InvestorProfile entity)
    {
       return new InvestorProfileDto
       {
           Id=entity.Id,
           OrganizationName=entity.OrganizationName,
           InvestorType=entity.Type.ToString(),
           Governorate=entity.Governorate!.Name!,
           GovernorateId=entity.GovernorateId,
           AddressDetail=entity.AddressDetail
       };
    }
}