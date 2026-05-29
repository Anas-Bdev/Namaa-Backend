using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Application.Features.Lands.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Queries.GetCrops;
public class GetCropsQueryHandler(IAppDbContext context) : IRequestHandler<GetCropsQuery, Result<List<CropDto>>>
{
    public async Task<Result<List<CropDto>>> Handle(GetCropsQuery request, CancellationToken cancellationToken)
    {
        var land = await context.Lands
            .AsNoTracking()
            .Select(l => new{l.GovernorateId,l.Id})
            .FirstOrDefaultAsync(l => l.Id == request.LandId, cancellationToken);

        if (land is null)
            return ApplicationErrors.LandNotFound;

        var localCrops = await context.Crops
            .AsNoTracking()
            .Where(c => c.GovernorateId==land.GovernorateId)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

        var distinctDropdownCrops = localCrops
                                  .GroupBy(c => c.Name)
                                  .Select(group => group.First())
                                  .OrderBy(c => c.Name)
                                  .ToList();


        return distinctDropdownCrops.ToDtos();
    }
}