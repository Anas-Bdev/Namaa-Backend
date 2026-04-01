using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Application.Features.Lands.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Land;

namespace Namaa.Application.Features.Lands.Commands.CreateLand;

public class CreateLandCommandHandler(IAppDbContext context) : IRequestHandler<CreateLandCommand, Result<LandDto>>
{
    public async Task<Result<LandDto>> Handle(CreateLandCommand request, CancellationToken cancellationToken)
    {
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
            request.IrrigationMethod
        );
       
       if(createLandResult.IsError)
       return createLandResult.Errors;
       context.Lands.Add(createLandResult.Value);
       await context.SaveChangesAsync(cancellationToken);
       var land=createLandResult.Value;
       var landWithDetails = await context.Lands
        .Include(l => l.Governorate)
        .Include(l => l.SoilType)
        .AsNoTracking()
        .FirstOrDefaultAsync(l => l.Id == land.Id, cancellationToken);
        return landWithDetails!.ToDto();
    }
}