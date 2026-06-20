using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Experts.Commands.ApproveExpert;

public class ApproveExpertCommandHandler(IAppDbContext context, IIdentityService identityService,INotificationService notificationService) : IRequestHandler<ApproveExpertCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ApproveExpertCommand request, CancellationToken cancellationToken)
    {
        var expertExists=await context.ExpertProfiles.AnyAsync(x => x.Id==request.ExpertId,cancellationToken);
        if(!expertExists)
        return ApplicationErrors.ExpertNotFound;
        var result=await identityService.UpdateUserStatusAsync(request.ExpertId.ToString(),UserStatus.Active);
        if(result.IsError)
        return result.Errors;
        await notificationService.SendNotificationAsync(
        userId: request.ExpertId,
        title: "Application Approved! 🎉",
        message: "Congratulations! Your expert profile has been approved. You can now start responding to farmer consultations.",
        type: "ExpertStatusChanged",
        referencedId: request.ExpertId
    );
        return Result.Updated;
    }
}