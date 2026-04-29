using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Commands.FailSeedingCycle;

public class FailSeedingCycleCommandHandler(IAppDbContext context) : IRequestHandler<FailSeedingCycleCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(FailSeedingCycleCommand request, CancellationToken cancellationToken)
    {
        var seedingCycle=await context.SeedingCycles.FindAsync([request.Id],cancellationToken);
        if(seedingCycle is null)
        return ApplicationErrors.SeedingCycleNotFound;
        var result=seedingCycle.Fail();
        if(result.IsError)
        return result.Errors;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}