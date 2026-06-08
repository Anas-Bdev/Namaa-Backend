using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Weather.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Application.Features.Weather.Queries.GetWeatherAlerts;

public class GetWeatherAlertsQueryHandler(IAppDbContext context,IWeatherService weatherService) : IRequestHandler<GetWeatherAlertsQuery, Result<List<WeatherAlertDto>>>
{
    public async Task<Result<List<WeatherAlertDto>>> Handle(GetWeatherAlertsQuery request, CancellationToken cancellationToken)
    {
        var vulnerableCycles = await context.SeedingCycles
        .AsNoTracking()
        .Include(sc => sc.Land)
        .Include(sc => sc.Crop)
        .Where(sc => sc.Land!.FarmerId == request.FarmerId)          
        .Where(sc => sc.Status ==CycleStatus.Active)                       
        .Where(sc => sc.Land!.EnvironmentType == EnvironmentType.OpenField)                 
        .ToListAsync(cancellationToken);

        if(!vulnerableCycles.Any())
        return new List<WeatherAlertDto>();

        var alerts=new List<WeatherAlertDto>();

        var uniqueLands = vulnerableCycles.Select(sc => sc.Land).DistinctBy(l => l!.Id).ToList();

        foreach(var land in uniqueLands)
        {
            var liveWeather=await weatherService.GetWeatherByCityAsync(land!.Latitude,land.Longitude,cancellationToken);
            var currentTemp=(decimal)liveWeather.TemperatureCelsius;

            var endangeredCropsOnThisLand=vulnerableCycles.Where(sc => sc.LandId==land.Id).Where(sc => currentTemp > sc.Crop!.MaxTemperature || currentTemp < sc.Crop!.MinTemperature);
            
            foreach(var cycle in endangeredCropsOnThisLand)
            {
                alerts.Add(new WeatherAlertDto
                {
                    SeedingCycleId=cycle.Id,
                    CropName=cycle.Crop!.Name!,
                    LandName=cycle.Land!.Name!,
                    CurrentTemperature=currentTemp,
                    MaxTolerableTemp=cycle.Crop.MaxTemperature,
                    MinTolerableTemp=cycle.Crop.MinTemperature,
                    AlertMessage=currentTemp > cycle.Crop.MaxTemperature 
                    ? $"HEAT WARNING: {land.Name} ({currentTemp}°C) exceeds {cycle.Crop.Name}'s max of {cycle.Crop.MaxTemperature}°C." 
                    : $"FROST WARNING: {land.Name} ({currentTemp}°C) drops below {cycle.Crop.Name}'s min of {cycle.Crop.MinTemperature}°C."
                });
            }
        }
        
        return alerts;

    }
}