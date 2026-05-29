using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Expert;

namespace Namaa.Application.Features.Experts.Commands.UpdateProfile;
public class UpdateExpertProfileCommandHandler(
    IAppDbContext context) 
    : IRequestHandler<UpdateExpertProfileCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateExpertProfileCommand request, CancellationToken cancellationToken)
    {
        var expert = await context.ExpertProfiles
            .Include(x => x.Availabilities)
            .FirstOrDefaultAsync(e => e.Id == request.UserId, cancellationToken);
            
        if (expert is null)
            return ApplicationErrors.ExpertNotFound;

        var newAvailabilityEntities = new List<ExpertAvailability>();
        foreach (var slot in request.Availabilities)
        {
            var slotResult = ExpertAvailability.Create(Guid.NewGuid(), slot.Day, slot.StartTime, slot.EndTime);
            if (slotResult.IsError) return slotResult.Errors;
            newAvailabilityEntities.Add(slotResult.Value);
        }

        var profileResult = expert.UpdateProfile(
            request.Specialization,
            request.YearsOfExperience,
            request.CityId,
            request.AddressDetail,
            request.CanVisitOnSite
        );
            
        if (profileResult.IsError) return profileResult.Errors;

        var expertUpdateAvailability = expert.ReplaceAvailabilities(newAvailabilityEntities);
        if (expertUpdateAvailability.IsError) return expertUpdateAvailability.Errors;

        await context.SaveChangesAsync(cancellationToken);
           
     return Result.Updated;
    }
}