using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
<<<<<<< HEAD
=======
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Application.Features.Experts.Mappers;
>>>>>>> dev-alaa
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Expert;

namespace Namaa.Application.Features.Experts.Commands.UpdateProfile;
public class UpdateExpertProfileCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository) 
<<<<<<< HEAD
    : IRequestHandler<UpdateExpertProfileCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateExpertProfileCommand request, CancellationToken cancellationToken)
=======
    : IRequestHandler<UpdateExpertProfileCommand, Result<ExpertProfileDto>>
{
    public async Task<Result<ExpertProfileDto>> Handle(UpdateExpertProfileCommand request, CancellationToken cancellationToken)
>>>>>>> dev-alaa
    {
        var expert = await context.ExpertProfiles
            .Include(x => x.Availabilities)
            .FirstOrDefaultAsync(e => e.Id == request.UserId, cancellationToken);
            
        if (expert is null)
            return ApplicationErrors.ExpertNotFound;

        var newAvailabilityEntities = new List<ExpertAvailability>();
        foreach (var slot in request.Availabilities)
        {
<<<<<<< HEAD
            var slotResult = ExpertAvailability.Create(Guid.NewGuid(), slot.Day, slot.StartTime, slot.EndTime);
=======
            var slotResult = ExpertAvailability.Create(Guid.NewGuid(), expert.Id, slot.Day, slot.StartTime, slot.EndTime);
>>>>>>> dev-alaa
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

        var expertUpdateAvailability = expert.UpdatedAvailability(newAvailabilityEntities);
        if (expertUpdateAvailability.IsError) return expertUpdateAvailability.Errors;

        await context.SaveChangesAsync(cancellationToken);

        var user = await userReadRepository.Query()
            .FirstOrDefaultAsync(u => u.Id == expert.Id, cancellationToken);
<<<<<<< HEAD
     return Result.Updated;
=======

        return expert.ToDto(user!.FullName);
>>>>>>> dev-alaa
    }
}