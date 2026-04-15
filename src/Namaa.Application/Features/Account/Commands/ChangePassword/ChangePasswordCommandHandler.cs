using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Commands.ChangePassword;
public class ChangePasswordCommandHandler(IIdentityService identityService) : IRequestHandler<ChangePasswordCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
    var result=await identityService.ChangePasswordAsync(request.UserId,request.CurrentPassword,request.NewPassword);
    if(result.IsSuccess)
    return result.Value;
    return result.Errors;
    }
}