using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Commands.HarvestSeedingCycle;

public class HarvestSeedingCycleCommandHandler(IAppDbContext context) : IRequestHandler<HarvestSeedingCycleCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(HarvestSeedingCycleCommand request, CancellationToken cancellationToken)
    {
        var seedingCycle=await context.SeedingCycles.FindAsync([request.Id],cancellationToken);
        if(seedingCycle is null)
        return ApplicationErrors.SeedingCycleNotFound;
        var harvestResult=seedingCycle.Harvest(request.ActualHarvestDate,request.ActualYield);
        if(harvestResult.IsError)
        return harvestResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}