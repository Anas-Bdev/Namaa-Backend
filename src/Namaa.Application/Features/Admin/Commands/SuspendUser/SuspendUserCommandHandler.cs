using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Admin.Commands.SuspendUser;

public class SuspendUserCommandHandler(IIdentityService identityService,INotificationService notificationService) : IRequestHandler<SuspendUserCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(SuspendUserCommand request, CancellationToken cancellationToken)
    {
        var suspend= await identityService.UpdateUserStatusAsync(request.UserId.ToString(),UserStatus.Suspended,request.Reason);

        if(suspend.IsError)
        return suspend.Errors;

       await notificationService.SendNotificationAsync(
       userId: request.UserId,
       title: "Account Suspended",
       message: $"Your account has been suspended. Reason: {request.Reason}. Please contact support for more information.",
       type: "AccountStatusChanged",
       referencedId: request.UserId
       );

       return Result.Updated;
    }
}