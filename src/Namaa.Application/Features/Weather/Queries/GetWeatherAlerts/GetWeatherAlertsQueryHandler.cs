using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Weather.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Application.Features.Weather.Queries.GetWeatherAlerts;

public class GetWeatherAlertsQueryHandler(IAppDbContext context, IWeatherService weatherService, IAiConsultantService aiConsultantService) : IRequestHandler<GetWeatherAlertsQuery, Result<List<WeatherAlertDto>>>
{
    public async Task<Result<List<WeatherAlertDto>>> Handle(GetWeatherAlertsQuery request, CancellationToken cancellationToken)
    {
        var vulnerableCycles = await context.SeedingCycles
            .AsNoTracking()
            .Include(sc => sc.Land)
            .Where(sc => sc.Land!.FarmerId == request.FarmerId)
            .Where(sc => sc.Status == CycleStatus.Active)
            .Where(sc => sc.EnvironmentType == EnvironmentType.OpenField)
            .ToListAsync(cancellationToken);

        if (!vulnerableCycles.Any())
            return new List<WeatherAlertDto>();

        var uniqueCropNames = vulnerableCycles.Select(sc => sc.CropName).Distinct();
        var thresholdCache = new Dictionary<string, CropThresholds>();

        foreach (var cropName in uniqueCropNames)
        {
            thresholdCache[cropName] = await aiConsultantService.GetCropTemperatureLimitsAsync(cropName, cancellationToken);
        }

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
            var next72HoursForecast = result.Forecast
                .Where(f => f.ForecastTime <= DateTime.UtcNow.AddDays(3))
                .ToList();

            var cropsOnThisLand = vulnerableCycles.Where(sc => sc.LandId == land.Id).ToList();
            
            foreach (var cycle in cropsOnThisLand)
            {
                var thresholds = thresholdCache[cycle.CropName];
                
                var dangerousInterval = next72HoursForecast
                    .OrderBy(f => Math.Abs((decimal)f.TemperatureCelsius - (thresholds.MaxTemp + thresholds.MinTemp) / 2))
                    .FirstOrDefault(f => (decimal)f.TemperatureCelsius > thresholds.MaxTemp 
                                      || (decimal)f.TemperatureCelsius < thresholds.MinTemp);

                if (dangerousInterval != null)
                {
                    var predictedTemp = (decimal)dangerousInterval.TemperatureCelsius;
                    var localTimeDisplay = dangerousInterval.ForecastTime.ToLocalTime().ToString("dd MMM, h:mm tt");
                    
                    alerts.Add(new WeatherAlertDto
                    {
                        SeedingCycleId = cycle.Id,
                        CropName = cycle.CropName,
                        LandName = land.Name!,
                        CurrentTemperature = predictedTemp,
                        MaxTolerableTemp = thresholds.MaxTemp,
                        MinTolerableTemp = thresholds.MinTemp,
                        AlertTime = dangerousInterval.ForecastTime,
                        AlertMessage = predictedTemp > thresholds.MaxTemp
                            ? $"PREDICTIVE HEAT WARNING: High temperatures ({predictedTemp}°C) are expected to exceed {cycle.CropName}'s max limit on {localTimeDisplay}."
                            : $"PREDICTIVE FROST WARNING: Freezing risk ({predictedTemp}°C) is expected to fall below {cycle.CropName}'s minimum on {localTimeDisplay}. Please apply crop shielding covers.",
                        AlertType = predictedTemp > thresholds.MaxTemp ? "Heat" : "Frost"
                    });
                }
            }
        }
        
        return alerts;
    }
}