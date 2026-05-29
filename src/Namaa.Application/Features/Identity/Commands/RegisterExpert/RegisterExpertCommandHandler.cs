using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Constants;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Expert;

namespace Namaa.Application.Features.Identity.Commands.RegisterExpert;
public class RegisterExpertCommandHandler(IIdentityService identityService, IFileService fileService,IAppDbContext context) : IRequestHandler<RegisterExpertCommand, Result<Created>>
{
    public async Task<Result<Created>> Handle(RegisterExpertCommand request, CancellationToken cancellationToken)
    {
    
        var identityResult = await identityService.CreateUserAsync(
            request.Password, 
            request.Email, 
            AppRoles.Expert,
            request.FirstName,
            request.LastName,
            request.PhoneNumber);

      if(identityResult.IsError)
      return identityResult.Errors;
      
      var userId=Guid.Parse(identityResult.Value);
      try{
      var cvUrl=await fileService.UploadFileAsync(request.CvFile,"expert-cvs",cancellationToken);
      var expertResult=ExpertProfile.Create(userId,cvUrl);
      if(expertResult.IsError){
        await identityService.DeleteUserAsync(identityResult.Value);
      return expertResult.Errors;
      }
      var expert=expertResult.Value;
       context.ExpertProfiles.Add(expert);
       await context.SaveChangesAsync(cancellationToken);
       return Result.Created;
     }

        catch
        {
            await identityService.DeleteUserAsync(identityResult.Value);
            throw;
        }


    }
}