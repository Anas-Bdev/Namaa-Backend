using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.Register;

// 1. Removed <TRequest, TResponse>
public class RegisterCommandHandler(
    IIdentityService service,
    IEmailSender sender,
    IEmailTemplateService emailTemplate) 
    : IRequestHandler<RegisterCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // 1. Create User
        var createResult = await service.CreateUserAsync(
            request.Password, 
            request.Email, 
            request.Role, 
            request.FirstName, 
            request.LastName, 
            request.PhoneNumber);
            
        if (!createResult.IsSuccess)
        {
            return createResult.Errors;
        }

        var userId = createResult.Value;

        // 2. Generate Link
        var linkResult = await service.GenerateConfirmationLinkAsync(userId);
        
        if (!linkResult.IsSuccess)
        {
            return linkResult.Errors;
        }

        // 3. Build Email Body (Renamed variable to 'emailBody')
        var emailBody = emailTemplate.BuildConfirmEmailBody(
            $"{request.FirstName} {request.LastName ?? string.Empty}".Trim(), 
            linkResult.Value);

        // 4. Send Email
        await sender.SendEmailAsync(
            request.Email, 
            "Confirm Your Email", // This is the actual Subject
            emailBody,            // This is the Body
            cancellationToken);

        return Result.Success;
    }
}