using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Experts.Queries.GetExperts;

public class GetExpertsQueryHandler(IAppDbContext context,IUserReadRepository userReadRepository) : IRequestHandler<GetExpertsQuery, Result<PaginatedList<ExpertSummaryDto>>>
{
    public async Task<Result<PaginatedList<ExpertSummaryDto>>> Handle(GetExpertsQuery request, CancellationToken cancellationToken)
    {
        var usersQuery=userReadRepository.Query();
        var query=from expert in context.ExpertProfiles.AsNoTracking()
                  join user in usersQuery on expert.Id equals user.Id
                  where user.Status==UserStatus.Active
                  select new {expert,user};

       if(request.Specialization.HasValue)
        {
            query=query.Where(x => x.expert.Specialization==request.Specialization);
        }

        if (request.CityId.HasValue)
        {
            query=query.Where(x => x.expert.GovernorateId==request.CityId);
        }
     
     var totalCount=await query.CountAsync(cancellationToken);
     var totalPages=(int)Math.Ceiling(totalCount/(double)request.PageSize);

     var items= await query.OrderByDescending(x => x.expert.YearsOfExperience)
               .ThenBy(x => x.expert.Id)
               .Skip((request.PageNumber-1)*request.PageSize)
               .Take(request.PageSize)
               .Select(x => new ExpertSummaryDto
               {
                Governorate=x.expert.Governorate!.Name!,
                Id=x.expert.Id,
                FullName=x.user.FullName,
                Specialization=x.expert.Specialization.ToString()!,
                GovernorateId=x.expert.Governorate.Id,
                YearsOfExperience=x.expert.YearsOfExperience ?? 0
           }).ToListAsync(cancellationToken);

           var PaginatedList=new PaginatedList<ExpertSummaryDto>
           {
               PageNumber=request.PageNumber,
               PageSize=request.PageSize,
               TotalCount=totalCount,
               TotalPages=totalPages,
               Items=items
     };

     return PaginatedList;
    }

        
    
    
}