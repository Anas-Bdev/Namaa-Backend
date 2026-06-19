using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Experts.Commands.RejectExpert;

public class RejectExpertCommandHandler(IAppDbContext context, IIdentityService identityService,INotificationService notificationService) : IRequestHandler<RejectExpertCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(RejectExpertCommand request, CancellationToken cancellationToken)
    {
         var expertExists=await context.ExpertProfiles.AnyAsync(x => x.Id==request.ExpertId,cancellationToken);
        if(!expertExists)
        return ApplicationErrors.ExpertNotFound;
        var result=await identityService.UpdateUserStatusAsync(request.ExpertId.ToString(),UserStatus.Rejected,request.Reason);
        if(result.IsError)
        return result.Errors;

        await notificationService.SendNotificationAsync(
            userId: request.ExpertId,
            title: "Application Status Update",
            message: $"We regret to inform you that your expert application was not approved. Reason: {request.Reason}",
            type: "ExpertStatusChanged",
            referencedId: request.ExpertId
        );
        return Result.Updated;
    }
}