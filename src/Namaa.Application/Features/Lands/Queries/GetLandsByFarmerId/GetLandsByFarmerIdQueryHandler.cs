using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Application.Features.Lands.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Queries.GetLandsByFarmerId;
public class GetLandsByFarmerIdQueryHandler(IAppDbContext context) : IRequestHandler<GetLandsByFarmerIdQuery, Result<List<LandDto>>>
{
    public async Task<Result<List<LandDto>>> Handle(GetLandsByFarmerIdQuery request, CancellationToken cancellationToken)
    {
      var lands=await context.Lands.Where(l => l.FarmerId==request.FarmerId).AsNoTracking().ToListAsync(cancellationToken);
    return lands.ToDtos();
    }
}