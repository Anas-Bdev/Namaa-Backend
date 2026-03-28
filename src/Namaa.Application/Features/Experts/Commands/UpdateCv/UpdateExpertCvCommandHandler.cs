using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Experts.Commands.UpdateCv;
public class UpdateExpertCvCommandHandler(IAppDbContext context, IFileService fileService) 
    : IRequestHandler<UpdateExpertCvCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateExpertCvCommand request, CancellationToken ct)
    {
        var expert = await context.ExpertProfiles
            .FirstOrDefaultAsync(x => x.Id == request.UserId, ct);

        if (expert is null)
            return ApplicationErrors.ExpertNotFound;

        var cvUrl = await fileService.UploadFileAsync(request.File, "expert-cvs", ct); 
        var updateResult = expert.UpdateCvUrl(cvUrl);
        
        if (updateResult.IsError)
        {
            return updateResult.Errors;
        }

         await context.SaveChangesAsync(ct);

        return Result.Updated;
    }
}