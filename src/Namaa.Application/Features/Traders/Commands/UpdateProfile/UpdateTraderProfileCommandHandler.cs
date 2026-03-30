using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Application.Features.Traders.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Trader;

namespace Namaa.Application.Features.Traders.Commands.UpdateProfile;

public class UpdateTraderProfileCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<UpdateTraderProfileCommand, Result<TraderProfileDto>>
{
    public async Task<Result<TraderProfileDto>> Handle(
        UpdateTraderProfileCommand request,
        CancellationToken cancellationToken)
    {
        var trader = await context.TraderProfiles
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (trader is null)
            return TraderErrors.TraderNotFound;

        var result = trader.UpdateProfile(
            request.BusinessName,
            request.BusinessType,
            request.PreferredCategories,
            request.CityId,
            request.AddressDetail
        );

        if (result.IsError)
            return result.Errors;

        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query()
            .ToListAsync(cancellationToken);

        var fullName = users.FirstOrDefault(u => u.Id == trader.Id)?.FullName ?? string.Empty;

        return trader.ToDto(fullName);
    }
}
