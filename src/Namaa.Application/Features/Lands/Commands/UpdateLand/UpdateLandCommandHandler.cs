using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Commands.UpdateLand;

public class UpdateLandCommandHandler(IAppDbContext context) : IRequestHandler<UpdateLandCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateLandCommand request, CancellationToken cancellationToken)
    {
        var land = await context.Lands.FindAsync([request.LandId], cancellationToken);
        if(land is null)
        return ApplicationErrors.LandNotFound;
        if (land.FarmerId != request.FarmerId)
    {
        return ApplicationErrors.Forbidden;
    }
        var updateResult = land.Update(
            request.CityId,
            request.SoilId,
            request.Name,
            request.AreaDonum,
            request.WaterSourceType,
            request.WaterAvailability,
            request.EnvironmentType
        );
        if(updateResult.IsError)
        return updateResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
         return Result.Updated;
    }
}