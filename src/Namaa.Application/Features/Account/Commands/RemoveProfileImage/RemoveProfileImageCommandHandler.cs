using System.Security.Principal;
using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Commands.RemvoeProfileImage;

public class RemoveProfileImageCommandHandler(IIdentityService identityService) : IRequestHandler<RemoveProfileImageCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(RemoveProfileImageCommand request, CancellationToken cancellationToken)
    {
        var result=await identityService.UpdateProfileImageUrlAsync(request.UserId!,null);
        if(result.IsError)
        return result.Errors;
        return Result.Updated;
    }
}