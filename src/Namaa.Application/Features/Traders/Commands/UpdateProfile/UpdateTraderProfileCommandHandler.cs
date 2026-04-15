using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Traders.Commands.UpdateProfile;

public class UpdateTraderProfileCommandHandler(IAppDbContext context) : IRequestHandler<UpdateTraderProfileCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateTraderProfileCommand request, CancellationToken cancellationToken)
    {
        var trader = await context.TraderProfiles
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        // 2. Guard: Does it exist?
        if (trader is null)
            return ApplicationErrors.TraderNotFound;

       
       var updateResult = trader.UpdateProfile(request.BusinessName,request.TraderType,request.GovernorateId,request.AddressDetail);

        if (updateResult.IsError)
            return updateResult.Errors;

        // 4. Save Changes
        await context.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}