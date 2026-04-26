using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.SeedingCycles.Dtos;
using Namaa.Application.Features.SeedingCycles.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Queries.GetMySeedingCycles;

public class GetMySeedingCyclesQueryHandler(IAppDbContext context) : IRequestHandler<GetMySeedingCyclesQuery, Result<List<SeedingCycleDto>>>
{
    public async Task<Result<List<SeedingCycleDto>>> Handle(GetMySeedingCyclesQuery request, CancellationToken cancellationToken)
    {
       var seedingCycles=await context.SeedingCycles
                                .AsNoTracking()
                                .Where(x => x.Land!.FarmerId==request.FarmerId)
                                .OrderByDescending(x => x.CreatedAtUtc)
                                .ToListAsync(cancellationToken);
      return seedingCycles.ToDtos();
    }
}