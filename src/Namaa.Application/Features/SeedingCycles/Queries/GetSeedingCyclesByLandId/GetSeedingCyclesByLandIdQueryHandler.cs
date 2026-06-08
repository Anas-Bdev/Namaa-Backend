using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.SeedingCycles.Dtos;
using Namaa.Application.Features.SeedingCycles.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Queries.GetSeedingCyclesByLandId;

public class GetSeedingCyclesByLandIdQueryHandler(IAppDbContext context) : IRequestHandler<GetSeedingCyclesByLandIdQuery, Result<List<SeedingCycleDto>>>
{
    public async Task<Result<List<SeedingCycleDto>>> Handle(GetSeedingCyclesByLandIdQuery request, CancellationToken cancellationToken)
    {
        var seedingCycles=await context.SeedingCycles.AsNoTracking().Where(sc => sc.LandId==request.LandId)
                                .Include(x => x.Land)
                                .Include(x => x.Crop)
                                .OrderByDescending(sc => sc.StartDate).ToListAsync(cancellationToken);
        
        return seedingCycles.ToDtos();
    }
}