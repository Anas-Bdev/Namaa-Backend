using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Experts.Queries.GetExpertProfileById;

public class GetExpertProfileByIdQueryHandler(IAppDbContext context, IUserReadRepository userReadRepository) : IRequestHandler<GetExpertProfileByIdQuery, Result<ExpertListItemDto>>
{
    public async Task<Result<ExpertListItemDto>> Handle(GetExpertProfileByIdQuery request, CancellationToken cancellationToken)
    {
          var expert=await context.ExpertProfiles.AsNoTracking().
          Include(x => x.Governorate).
          FirstOrDefaultAsync(f => f.Id==request.ExpertId && f.GovernorateId.HasValue,cancellationToken);
          
        if(expert is null)
        return ApplicationErrors.ExpertNotFound;
        var user=await userReadRepository.GetByIdAsync(request.ExpertId,cancellationToken);
         if (user is null || user.Status != UserStatus.Active)
        return ApplicationErrors.ExpertNotFound;

       return new ExpertListItemDto
        {
            Id = expert.Id,
            FullName = user.FullName,
            ProfileImageUrl = user.ProfileImageUrl,
            Governorate = expert.Governorate!.Name!,
            Specialization=expert.Specialization.ToString()!,
            YearsOfExperience=expert.YearsOfExperience!.Value,
            CanVisitOnSite=expert.CanVisitOnSite!.Value
        };
    }
}