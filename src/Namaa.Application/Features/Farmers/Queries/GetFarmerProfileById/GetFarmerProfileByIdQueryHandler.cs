using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Farmers.Queries.GetFarmerProfileById;

public class GetFarmerProfileByIdQueryHandler(IAppDbContext context,IUserReadRepository userReadRepository) : IRequestHandler<GetFarmerProfileByIdQuery, Result<FarmerListItemDto>>
{
    public async Task<Result<FarmerListItemDto>> Handle(GetFarmerProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var farmer=await context.FarmerProfiles.AsNoTracking().
        Include(x => x.Governorate)
        .FirstOrDefaultAsync(f => f.Id==request.FarmerId,cancellationToken);
        if(farmer is null)
        return ApplicationErrors.FarmerNotFound;
        var user=await userReadRepository.GetByIdAsync(request.FarmerId,cancellationToken);
         if (user is null || user.Status != UserStatus.Active)
            return ApplicationErrors.FarmerNotFound;

       return new FarmerListItemDto
        {
            Id = farmer.Id,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            ProfileImageUrl = user.ProfileImageUrl,
            Description = farmer.Description,
            Governorate = farmer.Governorate!.Name!,
            AiSummary=farmer.AiReviewSummary
        };
    }
}