using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Features.Experts.Mappers;


namespace Namaa.Application.Features.Experts.Queries.GetExpertProfile;
public class GetExpertProfileQueryHandler(IAppDbContext context) : IRequestHandler<GetExpertProfileQuery, Result<ExpertProfileDto>>
{

    public async Task<Result<ExpertProfileDto>> Handle(GetExpertProfileQuery request, CancellationToken cancellationToken)
    {
       var expert = await context.ExpertProfiles
        .Include(x => x.Governorate)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if(expert is null)
        return ApplicationErrors.ExpertNotFound;

        return expert.ToDto();
    }
}