using Namaa.Application.Features.Weather.Dtos;

namespace Namaa.Application.Common.Interfaces;
public interface IWeatherService
{
    Task<WeatherDto> GetWeatherByCityAsync(double latitude,double longitude,CancellationToken cancellationToken);
    Task<List<ForecastIntervalDto>> GetWeatherForecastAsync(double latitude, double longitude,CancellationToken cancellationToken);
}