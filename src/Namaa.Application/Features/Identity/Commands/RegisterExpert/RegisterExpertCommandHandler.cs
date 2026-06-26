using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Constants;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Expert;

namespace Namaa.Application.Features.Identity.Commands.RegisterExpert;
public class RegisterExpertCommandHandler(
    IIdentityService identityService, 
    IFileService fileService,
    IAppDbContext context,
    IEmailSender sender,                // <-- Added
    IEmailTemplateService emailTemplate ,
    INotificationService notificationService// <-- Added
    ) : IRequestHandler<RegisterExpertCommand, Result<Created>>
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

        if (identityResult.IsError)
            return identityResult.Errors;
      
        var userIdString = identityResult.Value;
        var userId = Guid.Parse(userIdString);
      
        try
        {
            var cvUrl = await fileService.UploadFileAsync(request.CvFile, "expert-cvs", cancellationToken);
            
            // Using your exact original method
            var expertResult = ExpertProfile.Create(userId, cvUrl); 
            
            if (expertResult.IsError)
            {
                await identityService.DeleteUserAsync(userIdString);
                return expertResult.Errors;
            }

            var expert = expertResult.Value;
            context.ExpertProfiles.Add(expert);
            await context.SaveChangesAsync(cancellationToken);

            // --- Added Missing Email Confirmation Flow ---
            var linkResult = await identityService.GenerateConfirmationLinkAsync(userIdString);
            
            if (linkResult.IsError)
            {
                // Rollback if link generation fails
                await identityService.DeleteUserAsync(userIdString);
                return linkResult.Errors;
            }

            var emailBody = emailTemplate.BuildConfirmEmailBody(
                $"{request.FirstName} {request.LastName ?? string.Empty}".Trim(), 
                linkResult.Value);

            await sender.SendEmailAsync(
                request.Email, 
                "Confirm Your Email", 
                emailBody,            
                cancellationToken);
            // ---------------------------------------------

            var adminsResult = await identityService.GetUsersInRoleAsync(AppRoles.Admin);
            var mainAdmin = adminsResult.IsSuccess ? adminsResult.Value.FirstOrDefault() : null;

            if (mainAdmin != null)
            {
                await notificationService.SendNotificationAsync(
                    userId: mainAdmin.UserId, 
                    title: "New Expert Registration 📋",
                    message: $"{request.FirstName} {request.LastName} has registered as an Expert and is waiting for approval.",
                    type: "ExpertPendingApproval",
                    referencedId: userId 
                );
            }

            return Result.Created;
        }
        catch
        {
            await identityService.DeleteUserAsync(userIdString);
            throw;
        }
    }
}