using System.Security.Principal;
using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.LogOut;

public class LogOutCommandHandler(IIdentityService identityService) : IRequestHandler<LogOutCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(LogOutCommand request, CancellationToken cancellationToken)
    {
        var result=await identityService.RevokeRefreshTokenAsync(request.UserId!,request.RefreshToken);
        if(!result.IsSuccess)
        return result.Errors;
        return result.Value;
    }
}