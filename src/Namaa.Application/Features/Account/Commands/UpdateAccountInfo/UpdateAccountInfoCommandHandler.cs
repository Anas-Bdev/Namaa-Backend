using MediatR;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Commands.UpdateAccountInfo;

public class UpdateAccountInfoCommandHandler(IIdentityService identityService) : IRequestHandler<UpdateAccountInfoCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateAccountInfoCommand request, CancellationToken cancellationToken)
    {
        var updateResult=await identityService.UpdateAccountInfoAsync(request.UserId,request.FirstName,request.LastName,request.PhoneNumber);
        if(updateResult.IsError)
        return updateResult.Errors;
        return Result.Updated;
    }
}