using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investors.Commands.UpdateProfile;

public class UpdateInvestorProfileCommandHandler(IAppDbContext context) : IRequestHandler<UpdateInvestorProfileCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateInvestorProfileCommand request, CancellationToken cancellationToken)
    {
        var investor = await context.InvestorProfiles
            .Include(x => x.Governorate) 
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

       if(investor is null)
       return ApplicationErrors.InvestorNotFound;

       var updateResult = investor.UpdateProfile(request.Type,request.OrganizationName,request.GovernorateId,request.AddressDetail);

    if (updateResult.IsError)
        return updateResult.Errors;

    // 3. Finalize: Save and return
    await context.SaveChangesAsync(cancellationToken);

    return Result.Updated;
    }
}