using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Experts.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Experts.Commands.UpdateCv;
public class UpdateExpertCvCommandHandler(IAppDbContext context, IFileService fileService,IIdentityService identityService) 
    : IRequestHandler<UpdateExpertCvCommand, Result<UpdateExpertCvDto>>
{
    public async Task<Result<UpdateExpertCvDto>> Handle(UpdateExpertCvCommand request, CancellationToken ct)
    {
        var expert = await context.ExpertProfiles.FindAsync([request.UserId], ct);
        if (expert is null) return ApplicationErrors.ExpertNotFound;

        var statusResult = await identityService.GetUserStatusAsync(request.UserId.ToString());
        if (statusResult.IsError) return statusResult.Errors;

        var currentStatus = statusResult.Value;
        if (currentStatus != UserStatus.Active && currentStatus != UserStatus.Pending)
        {
            return ApplicationErrors.InvalidStatusForUpdate;
        }

        if (currentStatus == UserStatus.Rejected)
        {
            var statusUpdateResult = await identityService.UpdateUserStatusAsync(request.UserId.ToString(), UserStatus.Pending);
            if (statusUpdateResult.IsError) return statusUpdateResult.Errors;
        }

        var cvUrl = await fileService.UploadFileAsync(request.File, "expert-cvs", ct); 
        
        var updateResult = expert.UpdateCvUrl(cvUrl);
        if (updateResult.IsError) return updateResult.Errors;
        
        await context.SaveChangesAsync(ct);

        return new UpdateExpertCvDto { CvUrl = cvUrl };
    }
}