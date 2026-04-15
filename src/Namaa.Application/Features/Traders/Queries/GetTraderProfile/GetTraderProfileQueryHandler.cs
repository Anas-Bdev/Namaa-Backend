using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Application.Features.Traders.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Trader;

namespace Namaa.Application.Features.Traders.Queries.GetTraderProfile;

public class GetTraderProfileQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetTraderProfileQuery, Result<TraderProfileDto>>
{
    public async Task<Result<TraderProfileDto>> Handle(
        GetTraderProfileQuery request,
        CancellationToken cancellationToken)
    {
        var trader = await context.TraderProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == request.UserId, cancellationToken);

        if (trader is null)
            return ApplicationErrors.TraderNotFound;

        var users = await userReadRepository.Query()
            .ToListAsync(cancellationToken);

        var fullName = users.FirstOrDefault(u => u.Id == trader.Id)?.FullName ?? string.Empty;

        return trader.ToDto(fullName);
    }
}