using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Commands.DeleteLand;

public class DeleteLandCommandHandler(IAppDbContext context) : IRequestHandler<DeleteLandCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteLandCommand request, CancellationToken cancellationToken)
    {
        var land = await context.Lands.FindAsync([request.LandId],cancellationToken);
        if(land is null)
        return ApplicationErrors.LandNotFound;

        if(land.FarmerId!=request.FarmerId)
        return ApplicationErrors.Forbidden;
        context.Lands.Remove(land);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Deleted;
    }
}