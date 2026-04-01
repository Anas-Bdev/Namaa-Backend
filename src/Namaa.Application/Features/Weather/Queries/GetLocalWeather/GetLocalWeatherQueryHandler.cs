using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Weather.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.ReferenceData;

namespace Namaa.Application.Features.Weather.Queries.GetLocalWeather;

public class GetLocalWeatherQueryHandler(IAppDbContext context,IWeatherService weatherService) : IRequestHandler<GetLocalWeatherQuery, Result<WeatherDto>>
{
    public async Task<Result<WeatherDto>> Handle(GetLocalWeatherQuery request, CancellationToken cancellationToken)
    {
        var land=await context.Lands
                .Include(l => l.Governorate)
               .AsNoTracking()
               .FirstOrDefaultAsync(l => l.Id==request.LandId && l.FarmerId==request.FarmerId);

               if(land is null)
               return ApplicationErrors.LandNotFound;
               
        

       return await weatherService.GetWeatherByCityAsync(land.Latitude,land.Longitude,cancellationToken);
    }
}