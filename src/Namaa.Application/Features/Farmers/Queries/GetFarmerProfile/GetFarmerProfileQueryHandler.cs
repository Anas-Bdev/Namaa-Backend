using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Application.Features.Farmers.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Profiles.Farmer;

namespace Namaa.Application.Features.Farmers.Queries.GetFarmerProfile;

public class GetFarmerProfileQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetFarmerProfileQuery, Result<FarmerProfileDto>>
{
    public async Task<Result<FarmerProfileDto>> Handle(
    GetFarmerProfileQuery request,
    CancellationToken cancellationToken)
    {
        var farmer = await context.FarmerProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);

        if (farmer is null)
            return FarmerErrors.FarmerNotFound;

        var users = await userReadRepository.Query()
            .ToListAsync(cancellationToken);

        var fullName = users.FirstOrDefault(u => u.Id == farmer.Id)?.FullName ?? string.Empty;

        return farmer.ToDto(fullName);
    }
}