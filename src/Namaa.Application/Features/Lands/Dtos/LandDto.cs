using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Lands.Dtos;
public class LandDto
{
    public Guid LandId {get;set;}
    public string Name {get;set;}=string.Empty;
    public double AreaDonum {get;set;}
    public int CityId {get;set;}
    public int SoilId {get;set;}
    public WaterSourceType WaterSourceType {get;set;}
    public WaterAvailability WaterAvailability {get;set;}
    public EnvironmentType EnvironmentType {get;set;}

}