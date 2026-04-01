using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Lookups.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lookups.Queries.GetGovernorates;

public class GetGovernoratesQueryHandler(IAppDbContext context) : IRequestHandler<GetGovernoratesQuery, Result<List<GovernorateDto>>>
{
    public async Task<Result<List<GovernorateDto>>> Handle(GetGovernoratesQuery request, CancellationToken cancellationToken)
    {
     var governorates = await context.Governorates
    .AsNoTracking() 
    .ToListAsync(cancellationToken);
    return governorates.ToDtos();
    }
}
