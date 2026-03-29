using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Profiles.Expert;

namespace Namaa.Application.Features.Experts.Mappers;
public static class ExpertMapper
{
    public static ExpertProfileDto ToDto(this ExpertProfile expert,string fullName)
    {
        return new ExpertProfileDto
        {
            Governorate=expert.Governorate!.Name!,
            Id=expert.Id,
            FullName=fullName,
            Specialization=expert.Specialization.ToString()!,
            YearsOfExperience=expert.YearsOfExperience ?? 0,
            GovernorateId=expert.GovernorateId ?? 0,
            AddressDetail=expert.AddressDetail!,
            CanVisitOnSite=expert.CanVisitOnSite ?? false,
            CvUrl=expert.CvUrl!,
            Availabilities=expert.Availabilities.Select(e => e.ToDto()).ToList()
        };
    }
    public static ExpertAvailabilityDto ToDto(this ExpertAvailability availability)
    {
        return new ExpertAvailabilityDto
        {
            Day=availability.Day.ToString(),
            StartTime=availability.StartTime,
            EndTime=availability.EndTime
        };
    }

   
}