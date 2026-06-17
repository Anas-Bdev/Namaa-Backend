using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Weather.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Application.Features.Weather.Queries.GetWeatherAlerts;

public class GetWeatherAlertsQueryHandler(IAppDbContext context, IWeatherService weatherService) : IRequestHandler<GetWeatherAlertsQuery, Result<List<WeatherAlertDto>>>
{
    public async Task<Result<List<WeatherAlertDto>>> Handle(GetWeatherAlertsQuery request, CancellationToken cancellationToken)
    {
        var vulnerableCycles = await context.SeedingCycles
            .AsNoTracking()
            .Include(sc => sc.Land)
            .Include(sc => sc.Crop)
            .Where(sc => sc.Land!.FarmerId == request.FarmerId)          
            .Where(sc => sc.Status == CycleStatus.Active)                      
            .Where(sc => sc.EnvironmentType==EnvironmentType.OpenField)                 
            .ToListAsync(cancellationToken);

        if (!vulnerableCycles.Any())
            return new List<WeatherAlertDto>();

        var alerts = new List<WeatherAlertDto>();
        var uniqueLands = vulnerableCycles.Select(sc => sc.Land).DistinctBy(l => l!.Id).ToList();

        var weatherTasks = uniqueLands.Select(async land =>
        {
            var forecast = await weatherService.GetWeatherForecastAsync(land!.Latitude, land.Longitude, cancellationToken);
            return (Land: land, Forecast: forecast);
        }).ToList();

        var weatherResults = await Task.WhenAll(weatherTasks);

        foreach (var result in weatherResults)
        {
            var land = result.Land;
            var next24HoursForecast = result.Forecast
                .Where(f => f.ForecastTime <= DateTime.UtcNow.AddDays(1))
                .ToList();

            var cropsOnThisLand = vulnerableCycles.Where(sc => sc.LandId == land.Id).ToList();            
            foreach (var cycle in cropsOnThisLand)
            {
                var dangerousInterval = next24HoursForecast
                    .FirstOrDefault(f => (decimal)f.TemperatureCelsius > cycle.Crop!.MaxTemperature 
                                      || (decimal)f.TemperatureCelsius < cycle.Crop!.MinTemperature);

                if (dangerousInterval != null)
                {
                    var predictedTemp = (decimal)dangerousInterval.TemperatureCelsius;
                    var localTimeDisplay = dangerousInterval.ForecastTime.ToLocalTime().ToString("dd MMM, h:mm tt");
                    
                    alerts.Add(new WeatherAlertDto
                    {
                        SeedingCycleId = cycle.Id,
                        CropName = cycle.Crop!.Name!,
                        LandName = land.Name!,
                        CurrentTemperature = predictedTemp, 
                        MaxTolerableTemp = cycle.Crop.MaxTemperature,
                        MinTolerableTemp = cycle.Crop.MinTemperature,
                        AlertTime = dangerousInterval.ForecastTime,
                        AlertMessage = predictedTemp > cycle.Crop.MaxTemperature 
                            ? $"PREDICTIVE HEAT WARNING: High temperatures ({predictedTemp}°C) are expected to exceed {cycle.Crop.Name}'s max limit on {localTimeDisplay}." 
                            : $"PREDICTIVE FROST WARNING: Freezing risk ({predictedTemp}°C) is expected to fall below {cycle.Crop.Name}'s minimum on {localTimeDisplay}. Please apply crop shielding covers.",
                        AlertType = predictedTemp > cycle.Crop.MaxTemperature ? "Heat" : "Frost"
                    });
                }
            }
        }
        
        return alerts;
    }
}