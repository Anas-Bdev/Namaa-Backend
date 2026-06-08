using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Farmers.Commands.UpdateProfile;

public class UpdateFarmerProfileCommandHandler(IAppDbContext context) : IRequestHandler<UpdateFarmerProfileCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateFarmerProfileCommand request, CancellationToken cancellationToken)
    {
       var farmer = await context.FarmerProfiles
            .Include(x => x.Governorate) 
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

       if(farmer is null)
       return ApplicationErrors.FarmerNotFound;

       var updateResult = farmer.UpdateProfile(request.GovernorateId,request.Description,request.AddressDetail);

    if (updateResult.IsError)
        return updateResult.Errors;

    // 3. Finalize: Save and return
    await context.SaveChangesAsync(cancellationToken);

    return Result.Updated;
    }
}