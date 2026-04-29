using MediatR;
using MediatR.Pipeline;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.ResendConfirmationEmail;

public class ResendConfirmationEmailCommandHandler(IIdentityService identityService,IEmailSender emailSender,IEmailTemplateService emailTemplate) : IRequestHandler<ResendConfirmationEmailCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var userResult=await identityService.GetUserByEmailAsync(request.Email);
        if(userResult.IsError)
        return userResult.Errors;
        var user=userResult.Value;
        if(await identityService.IsEmailConfirmedAsync(request.Email))
        return  Error.Validation("Auth.AlreadyConfirmed", "This account is already verified.");
        var linkResult = await identityService.GenerateConfirmationLinkAsync(user.UserId.ToString());
        
        if (linkResult.IsError)
        {
            return linkResult.Errors;
        }
        // Assuming your AppUserDto has FirstName and LastName properties!
        var emailBody = emailTemplate.BuildConfirmEmailBody(
            $"{user.FirstName} {user.LastName ?? string.Empty}".Trim(), 
            linkResult.Value);

        // 6. Send Email (Reusing your exact logic)
        await emailSender.SendEmailAsync(
            request.Email, 
            "Confirm Your Email", 
            emailBody,            
            cancellationToken);

        return Result.Success;
    }
}