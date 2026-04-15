using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Commands.ActivateSeedingCycle;
 public class ActivateSeedingCycleCommandHandler(IAppDbContext context) : IRequestHandler<ActivateSeedingCycleCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ActivateSeedingCycleCommand request, CancellationToken cancellationToken)
    {
        var seedingCycle=await context.SeedingCycles.FindAsync([request.Id],cancellationToken);
        if(seedingCycle is null)
        return ApplicationErrors.SeedingCycleNotFound;

        var result=seedingCycle.Activate();
        if(result.IsError)
        return result.Errors;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}
