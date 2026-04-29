using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.ResetPassword;

public class ResetPasswordCommandHandler(IIdentityService identityService) : IRequestHandler<ResetPasswordCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var result=await identityService.ResetPasswordAsync(request.Email,request.ResetCode,request.NewPassword);
        if(!result.IsSuccess)
        return result.Errors;
        return result.Value;
    }
}