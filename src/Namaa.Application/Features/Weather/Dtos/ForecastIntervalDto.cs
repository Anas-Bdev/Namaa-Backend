namespace Namaa.Application.Features.Weather.Dtos;
public class ForecastIntervalDto
{
    public DateTime ForecastTime { get; set; }
    public double TemperatureCelsius { get; set; }
    public string ConditionText { get; set; } = string.Empty;
}