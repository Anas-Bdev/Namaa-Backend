using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Application.Features.Lands.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Queries.GetLandById;

public class GetLandByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetLandByIdQuery, Result<LandDto>>
{
    public async Task<Result<LandDto>> Handle(GetLandByIdQuery request, CancellationToken cancellationToken)
    {
        var land=await context.Lands.AsNoTracking().FirstOrDefaultAsync(l => l.Id==request.LandId,cancellationToken);
        if(land is null)
        return ApplicationErrors.LandNotFound;
        if (land.FarmerId != request.FarmerId)
        {
            return ApplicationErrors.Forbidden;
        }
        return land.ToDto();
    }
}