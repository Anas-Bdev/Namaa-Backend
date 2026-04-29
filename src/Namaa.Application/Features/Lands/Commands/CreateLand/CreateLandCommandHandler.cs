using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Application.Features.Lands.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Lands;

namespace Namaa.Application.Features.Lands.Commands.CreateLand;

public class CreateLandCommandHandler(IAppDbContext context, IGeocodingService geocodingService) 
    : IRequestHandler<CreateLandCommand, Result<LandDto>>
{
    public async Task<Result<LandDto>> Handle(CreateLandCommand request, CancellationToken cancellationToken)
    {
        // 1. External API Call (Get the hidden coordinates)
        var coordinates = await geocodingService.GetCoordinatesAsync(request.AddressDetail, cancellationToken);
        
        if (coordinates is null)
            return ApplicationErrors.AddressNotFound; // 👈 Explicit Failure

        var (latitude, longitude) = coordinates.Value;

        // 2. Domain Logic (Create the entity securely)
        var createLandResult = Land.Create(
            Guid.NewGuid(),
            request.FarmerId,
            request.CityId,
            request.SoilId,
            request.Name,
            request.AreaDonum,
            request.WaterSourceType,
            request.WaterAvailability,
            request.EnvironmentType,
            request.IrrigationMethod,
            latitude,
            longitude,
            request.AddressDetail
        );
       
        if (createLandResult.IsError)
            return createLandResult.Errors;
            
        var land = createLandResult.Value;

        context.Lands.Add(land);
        await context.SaveChangesAsync(cancellationToken);

        var landWithDetails = await context.Lands
            .Include(l => l.Governorate)
            .Include(l => l.SoilType)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == land.Id, cancellationToken);

        return landWithDetails!.ToDto();
    }
}