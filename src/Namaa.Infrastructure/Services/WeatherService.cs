using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Weather.Dtos;

namespace Namaa.Infrastructure.Services;

public class WeatherService(IConfiguration configuration,HttpClient httpClient) : IWeatherService
{
    public async Task<WeatherDto> GetWeatherByCityAsync(double latitude,double longitude, CancellationToken cancellationToken)
    {
      var apiKey=configuration["WeatherApi:OpenWeatherMapKey"];
      var url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&units=metric&appid={apiKey}";
      var response=await httpClient.GetFromJsonAsync<OpenWeatherResponse>(url,cancellationToken);
      var dto = new WeatherDto
            {
                TemperatureCelsius = response!.Main.Temp,
                ConditionText = response.Weather[0].Description,
                IconCode = response.Weather[0].Icon,
                Humidity=response.Main.Humidity,
                Pressure=response.Main.Pressure,
                WindSpeed=response.Wind.Speed
            };
            return dto;
    }



    private class OpenWeatherResponse
    {
        public MainData Main { get; set; } = default!;
        public WeatherData[] Weather { get; set; } = Array.Empty<WeatherData>();
        public WindData Wind {get;set;}=default!;
    }

    private class MainData 
    { 
        public double Temp { get; set; } 
        public int Humidity {get;set;}
        public int Pressure {get;set;}
    }

    private class WeatherData 
    { 
        public string Description { get; set; } = default!; 
        public string Icon { get; set; } = default!; 
    }

    private class WindData
    {
        public double Speed { get; set; }
    }
}