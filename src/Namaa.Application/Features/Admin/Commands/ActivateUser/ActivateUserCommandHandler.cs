using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Admin.Commands.ActivateUser;

public class ActivateUserCommandHandler(IIdentityService identityService,INotificationService notificationService) : IRequestHandler<ActivateUserCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var activate=await identityService.UpdateUserStatusAsync(request.UserId.ToString(),UserStatus.Active);

        if (activate.IsError)
        {
            return activate.Errors;
        }

        await notificationService.SendNotificationAsync(
        userId: request.UserId,
        title: "Account Activated",
        message: "Your account has been activated. You can now access all NAMA'A features.",
        type: "AccountStatusChanged",
        referencedId: request.UserId
        );
        
        return Result.Updated;
    }
}