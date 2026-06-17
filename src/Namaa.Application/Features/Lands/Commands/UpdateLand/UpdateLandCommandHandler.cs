using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Commands.UpdateLand;

public class UpdateLandCommandHandler(IAppDbContext context, IGeocodingService geocodingService) 
    : IRequestHandler<UpdateLandCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateLandCommand request, CancellationToken cancellationToken)
    {
        var land = await context.Lands.FindAsync([request.LandId], cancellationToken);
        
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
        var updateResult = land.Update(
            request.CityId,
            request.SoilId,
            request.Name,
            request.AreaDonum,
            request.WaterSourceType,
            request.WaterAvailability,
            latitude,
            longitude,
            request.AddressDetail
        );

        if (updateResult.IsError)
            return updateResult.Errors;

        // Persist changes
        await context.SaveChangesAsync(cancellationToken);
        
        return Result.Updated;
    }
}