using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Expert;

namespace Namaa.Application.Features.Experts.Commands.CreateProfile;

public class CreateExpertProfileCommandHandler(IAppDbContext context,IFileService fileService) : IRequestHandler<CreateExpertProfileCommand, Result<Created>>
{
    public async Task<Result<Created>> Handle(CreateExpertProfileCommand request, CancellationToken cancellationToken)
    {
        var exists = await context.ExpertProfiles.AnyAsync(x => x.Id == request.UserId, cancellationToken);
         if (exists) 
        return ApplicationErrors.ExpertAlreadyExists;
        var cvUrl=await fileService.UploadFileAsync(request.File,"expert-cvs",cancellationToken);
        var result=ExpertProfile.Create(request.UserId,cvUrl);
        if(result.IsError)
        return result.Errors;
        context.ExpertProfiles.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Created;
    }
}