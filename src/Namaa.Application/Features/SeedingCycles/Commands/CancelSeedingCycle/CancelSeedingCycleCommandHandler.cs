using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Commands.CancelSeedingCycle;

public class CancelSeedingCycleCommandHandler(IAppDbContext context) : IRequestHandler<CancelSeedingCycleCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(CancelSeedingCycleCommand request, CancellationToken cancellationToken)
    {
        var seedingCycle=await context.SeedingCycles.FindAsync([request.Id],cancellationToken);
        if(seedingCycle is null)
        return ApplicationErrors.SeedingCycleNotFound;
        var result=seedingCycle.Cancel();
        if(result.IsError)
        return result.Errors;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}