using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Application.Features.Traders.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Trader;

namespace Namaa.Application.Features.Traders.Commands.CreateProfile;

public class CreateTraderProfileCommandHandler(IAppDbContext context) : IRequestHandler<CreateTraderProfileCommand, Result<TraderProfileDto>>
{
    public async Task<Result<TraderProfileDto>> Handle(CreateTraderProfileCommand request, CancellationToken cancellationToken)
    {
         var exists = await context.TraderProfiles
            .AnyAsync(x => x.Id == request.UserId, cancellationToken);

        if (exists)
            return ApplicationErrors.AlreadyExists;

        var result=TraderProfile.Create(request.UserId,request.BusinessName,request.TraderType,request.GovernorateId,request.AddressDetail);

         if (result.IsError)
            return result.Errors;

        context.TraderProfiles.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);

         var trader = await context.TraderProfiles
        .Include(x => x.Governorate)
        .FirstAsync(x => x.Id == request.UserId, cancellationToken);

      return trader.ToDto();
        
        
    }
}
