using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.ChangePassword;

public class ChangePasswordCommandHandler(IIdentityService identityService) : IRequestHandler<ChangePasswordCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
    var result=await identityService.ChangePasswordAsync(request.UserId,request.CurrentPassword,request.NewPassword);
    if(result.IsSuccess)
    return result.Value;
    return result.Errors;
    }
}