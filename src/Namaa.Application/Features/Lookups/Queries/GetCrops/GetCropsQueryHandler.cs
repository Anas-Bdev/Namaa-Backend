using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Lookups.Dtos;
using Namaa.Application.Features.Lookups.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lookups.Queries.GetCrops;

public class GetCropsQueryHandler(IAppDbContext context) : IRequestHandler<GetCropsQuery, Result<List<CropDto>>>
{
    public async Task<Result<List<CropDto>>> Handle(GetCropsQuery request, CancellationToken cancellationToken)
    {
        var land = await context.Lands
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == request.LandId, cancellationToken);

        if (land is null)
            return ApplicationErrors.LandNotFound;

        var allCrops = await context.Crops
            .AsNoTracking()
            .ToListAsync(cancellationToken);

       
        var selectedCrops = allCrops
            .GroupBy(c => c.Name)
            .Select(group => 
                group.FirstOrDefault(c => c.GovernorateId == land.GovernorateId) 
                ?? group.First() 
            )
            .OrderBy(c => c.Name);

        return selectedCrops.ToDtos();
    }
}