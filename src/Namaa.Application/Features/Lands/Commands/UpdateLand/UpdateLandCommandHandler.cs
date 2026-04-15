using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Commands.UpdateLand;

<<<<<<< HEAD
public class UpdateLandCommandHandler(IAppDbContext context, IGeocodingService geocodingService) 
    : IRequestHandler<UpdateLandCommand, Result<Updated>>
=======
public class UpdateLandCommandHandler(IAppDbContext context) : IRequestHandler<UpdateLandCommand, Result<Updated>>
>>>>>>> dev-alaa
{
    public async Task<Result<Updated>> Handle(UpdateLandCommand request, CancellationToken cancellationToken)
    {
        var land = await context.Lands.FindAsync([request.LandId], cancellationToken);
<<<<<<< HEAD
        
        if (land is null)
            return ApplicationErrors.LandNotFound;
            
        // Security check: Ensure the user actually owns this land
        if (land.FarmerId != request.FarmerId)
            return ApplicationErrors.Forbidden;

        // Default to existing coordinates
        double latitude = land.Latitude;
        double longitude = land.Longitude;

        // The "Dirty Check": Only call the external API if the address changed
        if (land.AddressDetail != request.AddressDetail)
        {
            var coordinates = await geocodingService.GetCoordinatesAsync(request.AddressDetail, cancellationToken);
            
            if (coordinates is null)
                return ApplicationErrors.AddressNotFound;

            latitude = coordinates.Value.Latitude;
            longitude = coordinates.Value.Longitude;
        }

        // Apply domain rules
=======
        if(land is null)
        return ApplicationErrors.LandNotFound;
        if (land.FarmerId != request.FarmerId)
    {
        return ApplicationErrors.Forbidden;
    }
>>>>>>> dev-alaa
        var updateResult = land.Update(
            request.CityId,
            request.SoilId,
            request.Name,
            request.AreaDonum,
            request.WaterSourceType,
            request.WaterAvailability,
            request.EnvironmentType,
<<<<<<< HEAD
            request.IrrigationMethod,
            latitude,
            longitude,
            request.AddressDetail
        );

        if (updateResult.IsError)
            return updateResult.Errors;

        // Persist changes
        await context.SaveChangesAsync(cancellationToken);
        
        return Result.Updated;
=======
            request.IrrigationMethod
        );
        if(updateResult.IsError)
        return updateResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
         return Result.Updated;
>>>>>>> dev-alaa
    }
}