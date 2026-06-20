using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Traders.Queries.GetTraderProfileById;

public class GetTraderProfileQueryHandler(IAppDbContext context, IUserReadRepository userReadRepository) : IRequestHandler<GetTraderProfileByIdQuery, Result<TraderListItemDto>>
{
    public async Task<Result<TraderListItemDto>> Handle(GetTraderProfileByIdQuery request, CancellationToken cancellationToken)
    {
            var trader=await context.TraderProfiles.AsNoTracking().
          Include(x => x.Governorate).
          FirstOrDefaultAsync(f => f.Id==request.TraderId,cancellationToken);
          
        if(trader is null)
        return ApplicationErrors.TraderNotFound;
        var user=await userReadRepository.GetByIdAsync(request.TraderId,cancellationToken);
         if (user is null || user.Status != UserStatus.Active)
          return ApplicationErrors.ExpertNotFound;

       return new TraderListItemDto
        {
            Id = trader.Id,
            FullName = user.FullName,
            ProfileImageUrl = user.ProfileImageUrl,
            Governorate = trader.Governorate!.Name!,
            BusinessName=trader.BusinessName!,
            BusinessType=trader.BusinessType.ToString()        
            };
    }
}