namespace Namaa.Application.Features.Weather.Dtos;
public class WeatherDto
{
    public double TemperatureCelsius {get;set;}
    public string ConditionText {get;set;}=string.Empty;
    public string IconCode {get;set;}=string.Empty;
    public int Humidity {get;set;}
    public double WindSpeed {get;set;}
    public int Pressure {get;set;}
    
}