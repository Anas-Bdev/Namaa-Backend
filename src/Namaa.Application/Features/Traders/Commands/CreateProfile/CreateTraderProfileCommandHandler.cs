using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Trader;

namespace Namaa.Application.Features.Traders.Commands.CreateProfile;

public class CreateTraderProfileCommandHandler(IAppDbContext context)
    : IRequestHandler<CreateTraderProfileCommand, Result<Created>>
{
    public async Task<Result<Created>> Handle(
        CreateTraderProfileCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await context.TraderProfiles
            .AnyAsync(x => x.Id == request.UserId, cancellationToken);

        if (exists)
            return TraderErrors.TraderAlreadyExists;

        var result = TraderProfile.Create(request.UserId);

        if (result.IsError)
            return result.Errors;

        context.TraderProfiles.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Created;
    }
}
