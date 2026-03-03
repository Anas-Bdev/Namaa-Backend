using System.Security.Principal;
using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler(IIdentityService service,IEmailTemplateService emailTemplate,IEmailSender sender) : IRequestHandler<ForgotPasswordCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var codeResult=await service.GeneratePasswordResetCodeAsync(request.Email);
        if(!codeResult.IsSuccess) 
        return codeResult.Errors;
         var userResult=await service.GetUserByEmailAsync(request.Email);
         if(!userResult.IsSuccess)
         return userResult.Errors;
         var resetCode=codeResult.Value;
         var userName=userResult.Value.FullName;
         var emailBody=emailTemplate.BuildForgotPasswordBody(userName,resetCode);
         await sender.SendEmailAsync(request.Email,"Password Reset Request.",emailBody,cancellationToken);
         return Result.Success;
    }
}