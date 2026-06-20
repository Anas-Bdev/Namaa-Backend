using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Admin.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Admin.Queries.GetDashboardStatistics;

public class GetDashboardStatisticsQueryHandler(IUserReadRepository userReadRepository) : IRequestHandler<GetDashboardStatisticsQuery, Result<List<UserStatusCountDto>>>
{
    public async Task<Result<List<UserStatusCountDto>>> Handle(GetDashboardStatisticsQuery request, CancellationToken cancellationToken)
    {
      var users=userReadRepository.Query();

      return await users.GroupBy(u => u.Status)
                         .Select(g => new UserStatusCountDto
                         {
                            Status=g.Key,
                            Count=g.Count()
                         })
                         .ToListAsync(cancellationToken);
    }
}