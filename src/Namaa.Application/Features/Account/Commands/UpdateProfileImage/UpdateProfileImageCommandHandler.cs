using System.Security.Principal;
using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Commands.UpdateProfileImage;

public class UpdateProfileImageCommandHandler(IIdentityService identityService,IFileService fileService) : IRequestHandler<UpdateProfileImageCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateProfileImageCommand request, CancellationToken cancellationToken)
    {
     var result=await fileService.UploadFileAsync(request.FormFile,"users/profile-images");
     var updateResult=await identityService.UpdateProfileImageUrlAsync(request.UserId,result);
     if(updateResult.IsError)
     return updateResult.Errors;
     return Result.Updated;
    }
}