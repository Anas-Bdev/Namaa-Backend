using System.ComponentModel.DataAnnotations;
using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Commands.UpdateSeedingCycle;

public class UpdateSeedingCycleCommandHandler(IAppDbContext context) : IRequestHandler<UpdateSeedingCycleCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateSeedingCycleCommand request, CancellationToken cancellationToken)
    {
        var seedingCycle=await context.SeedingCycles.FindAsync([request.Id],cancellationToken);
        if(seedingCycle is null)
        return ApplicationErrors.SeedingCycleNotFound;

        var updateResult=seedingCycle.Update(request.StartDate,request.EstimatedHarvestDate,request.SeedQuantity,request.SeedingArea,request.ExpectedYield);
         if(updateResult.IsError)
         return updateResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;


    }
}