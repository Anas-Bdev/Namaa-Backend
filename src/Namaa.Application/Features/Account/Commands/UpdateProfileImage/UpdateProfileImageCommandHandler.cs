using System.Security.Principal;
using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Account.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Commands.UpdateProfileImage;

public class UpdateProfileImageCommandHandler(IIdentityService identityService,IFileService fileService) : IRequestHandler<UpdateProfileImageCommand, Result<UploadImageDto>>
{
    public async Task<Result<UploadImageDto>> Handle(UpdateProfileImageCommand request, CancellationToken cancellationToken)
    {
     var imageUrl=await fileService.UploadFileAsync(request.FormFile,"users/profile-images",cancellationToken);
     var updateResult=await identityService.UpdateProfileImageUrlAsync(request.UserId,imageUrl);
     if(updateResult.IsError)
     return updateResult.Errors;
     return new UploadImageDto
     {
         ImageUrl=imageUrl
     };
     
    }
}