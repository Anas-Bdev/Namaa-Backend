using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Application.Features.Farmers.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Farmer;

namespace Namaa.Application.Features.Farmers.Commands.UpdateProfile;

public class UpdateFarmerProfileCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<UpdateFarmerProfileCommand, Result<FarmerProfileDto>>
{
    public async Task<Result<FarmerProfileDto>> Handle(
        UpdateFarmerProfileCommand request,
        CancellationToken cancellationToken)
    {
        var farmer = await context.FarmerProfiles
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (farmer is null)
            return FarmerErrors.FarmerNotFound;

        var result = farmer.UpdateProfile(
            request.Description,
            request.CityId,
            request.AddressDetail,
            request.ExperienceLevel
        );

        if (result.IsError)
            return result.Errors;

        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query()
    .ToListAsync(cancellationToken);

        var fullName = users.FirstOrDefault(u => u.Id == farmer.Id)?.FullName ?? string.Empty;

        return farmer.ToDto(fullName);
    }
}
