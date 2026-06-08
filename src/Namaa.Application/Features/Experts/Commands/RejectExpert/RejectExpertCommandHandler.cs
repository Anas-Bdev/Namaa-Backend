using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Experts.Commands.RejectExpert;

public class RejectExpertCommandHandler(IAppDbContext context, IIdentityService identityService) : IRequestHandler<RejectExpertCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(RejectExpertCommand request, CancellationToken cancellationToken)
    {
         var expertExists=await context.ExpertProfiles.AnyAsync(x => x.Id==request.ExpertId,cancellationToken);
        if(!expertExists)
        return ApplicationErrors.ExpertNotFound;
        var result=await identityService.UpdateUserStatusAsync(request.ExpertId.ToString(),UserStatus.Rejected,request.Reason);
        if(result.IsError)
        return result.Errors;
        return Result.Updated;
    }
}