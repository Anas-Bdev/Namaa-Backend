using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Application.Features.Traders.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Traders.Queries.GetTraderProfile;

public class GetTraderProfileQueryHandler(IAppDbContext context) : IRequestHandler<GetTraderProfileQuery, Result<TraderProfileDto>>
{
    public async Task<Result<TraderProfileDto>> Handle(GetTraderProfileQuery request, CancellationToken cancellationToken)
    {
        var trader = await context.TraderProfiles
        .Include(x => x.Governorate)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if(trader is null)
        return ApplicationErrors.TraderNotFound;

        return trader.ToDto();
    }
}