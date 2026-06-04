using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Admin.Commands.ActivateUser;

public class ActivateUserCommandHandler(IIdentityService identityService) : IRequestHandler<ActivateUserCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        return await identityService.UpdateUserStatusAsync(request.UserId.ToString(),UserStatus.Active);
    }
}