using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Farmers.Dtos;
using Namaa.Application.Features.Farmers.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Farmers.Queries.GetFarmerProfile;

public class GetFarmerProfileQueryHandler(IAppDbContext context) : IRequestHandler<GetFarmerProfileQuery, Result<FarmerProfileDto>>
{
    public async Task<Result<FarmerProfileDto>> Handle(GetFarmerProfileQuery request, CancellationToken cancellationToken)
    {
      var farmer = await context.FarmerProfiles
        .Include(x => x.Governorate)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if(farmer is null)
        return ApplicationErrors.FarmerNotFound;

        return farmer.ToDto();
    }
}