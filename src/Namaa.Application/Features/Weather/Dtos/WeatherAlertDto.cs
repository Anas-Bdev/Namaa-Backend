namespace Namaa.Application.Features.Weather.Dtos;
public class WeatherAlertDto
{
    public Guid SeedingCycleId {get;set;}
    public string CropName {get;set;}=string.Empty;
    public string LandName {get;set;}=string.Empty;
    public decimal CurrentTemperature {get;set;}
    public decimal MaxTolerableTemp {get;set;}
    public decimal MinTolerableTemp {get;set;}
    public string AlertMessage {get;set;}=string.Empty;
    public DateTime AlertTime { get; set; }
    public string AlertType { get; set; } = string.Empty; 
}