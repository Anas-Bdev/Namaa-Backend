using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Admin.Commands.SuspendUser;

public class SuspendUserCommandHandler(IIdentityService identityService) : IRequestHandler<SuspendUserCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(SuspendUserCommand request, CancellationToken cancellationToken)
    {
        return await identityService.UpdateUserStatusAsync(request.UserId.ToString(),UserStatus.Suspended,request.Reason);
    }
}