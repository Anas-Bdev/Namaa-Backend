using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.SeedingCycles.Dtos;
using Namaa.Application.Features.SeedingCycles.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Queries.GetSeedingCycleById;

public class GetSeedingCycleByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetSeedingCycleByIdQuery, Result<SeedingCycleDto>>
{
    public async Task<Result<SeedingCycleDto>> Handle(GetSeedingCycleByIdQuery request, CancellationToken cancellationToken)
    {
        var seedingCycle=await context.SeedingCycles.AsNoTracking()
        .Include(x => x.Land)
        .FirstOrDefaultAsync(sc => sc.Id==request.Id,cancellationToken);
        if(seedingCycle is null)
        return ApplicationErrors.SeedingCycleNotFound;
        return seedingCycle.ToDto();
    }
}