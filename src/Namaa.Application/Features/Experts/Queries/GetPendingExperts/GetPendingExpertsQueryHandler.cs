using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Experts.Queries.GetPendingExperts;

public class GetPendingExpertsQueryHandler(IAppDbContext context, IUserReadRepository userReadRepository) : IRequestHandler<GetPendingExpertsQuery, Result<List<PendingExpertDto>>>
{
    public async Task<Result<List<PendingExpertDto>>> Handle(GetPendingExpertsQuery request, CancellationToken cancellationToken)
    {
        var pendingExperts=await userReadRepository.Query().AsNoTracking()
                  .Where(x => x.Status==UserStatus.Pending)
                  .Join(
                  context.ExpertProfiles.AsNoTracking(),
                  user => user.Id,
                  expert => expert.Id,
                 (user,expert) => new PendingExpertDto
                 {
                 UserId=user.Id,
                 FullName=user.FullName,
                 Email=user.Email!,
                 PhoneNumber=user.PhoneNumber,
                 CvUrl=expert.CvUrl!
                 }
                 )
                .ToListAsync(cancellationToken);

      return pendingExperts;
    }
}