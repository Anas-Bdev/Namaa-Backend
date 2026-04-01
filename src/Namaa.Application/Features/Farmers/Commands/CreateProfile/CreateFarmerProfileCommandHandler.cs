using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Farmer;

namespace Namaa.Application.Features.Farmers.Commands.CreateProfile;

public class CreateFarmerProfileCommandHandler(IAppDbContext context)
    : IRequestHandler<CreateFarmerProfileCommand, Result<Created>>
{
    public async Task<Result<Created>> Handle(
        CreateFarmerProfileCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await context.FarmerProfiles
            .AnyAsync(x => x.Id == request.UserId, cancellationToken);

        if (exists)
            return ApplicationErrors.FarmerAlreadyExists;

        var result = FarmerProfile.Create(request.UserId);

        if (result.IsError)
            return result.Errors;

        context.FarmerProfiles.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Created;
    }
}