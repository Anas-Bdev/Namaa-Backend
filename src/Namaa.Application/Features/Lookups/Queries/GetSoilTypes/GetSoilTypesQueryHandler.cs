using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Lookups.Dtos;
using Namaa.Application.Features.Lookups.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lookups.Queries.GetSoilTypes;

public class GetSoilTypesQueryHandler(IAppDbContext context) : IRequestHandler<GetSoilTypesQuery, Result<List<SoilTypeDto>>>{
    public async Task<Result<List<SoilTypeDto>>> Handle(GetSoilTypesQuery request, CancellationToken cancellationToken)
    {
        var soilTypes=await context.SoilTypes
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);

          return soilTypes.ToDtos();
    }
}