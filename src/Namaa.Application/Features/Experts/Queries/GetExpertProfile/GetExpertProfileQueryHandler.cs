using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Features.Experts.Mappers;


namespace Namaa.Application.Features.Experts.Queries.GetExpertProfile;
public class GetExpertProfileQueryHandler(IAppDbContext context,IUserReadRepository userReadRepository) : IRequestHandler<GetExpertProfileQuery, Result<ExpertProfileDto>>
{

    public async Task<Result<ExpertProfileDto>> Handle(GetExpertProfileQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = userReadRepository.Query();

        var query = from expert in context.ExpertProfiles
                        .Include(x => x.Availabilities) 
                        .AsNoTracking()
                    join user in usersQuery on expert.Id equals user.Id
                    where expert.Id == request.UserId 
                    select new { expert, user };

        var result = await query.FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            
            return ApplicationErrors.ExpertNotFound; 
        }

        
           

        return result.expert.ToDto(result.user.FullName);
    }
}