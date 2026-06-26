using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Profiles.Expert;
using Namaa.Domain.Common;


namespace Namaa.Application.Features.Experts.Mappers;
public static class ExpertMapper
{
    public static ExpertProfileDto ToDto(this ExpertProfile expert)
    {
        return new ExpertProfileDto
        {
            Governorate=expert.Governorate?.Name!,
            Id=expert.Id,
            Specialization=expert.Specialization!.ToSpacedName(),
            YearsOfExperience=expert.YearsOfExperience,
            GovernorateId=expert.GovernorateId,
            AddressDetail=expert.AddressDetail!,
            CanVisitOnSite=expert.CanVisitOnSite,
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