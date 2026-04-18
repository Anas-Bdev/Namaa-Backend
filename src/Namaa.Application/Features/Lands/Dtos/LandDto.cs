using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Lands.Dtos;
public class LandDto
{
<<<<<<< HEAD
    public string AddressDetail {get;set;}=string.Empty;
=======
>>>>>>> dev-alaa
    public Guid LandId {get;set;}
    public string Name {get;set;}=string.Empty;
    public double AreaDonum {get;set;}
    public int GovernorateId {get;set;}
    public int SoilTypeId {get;set;}
    public string GovernorateName {get;set;}=string.Empty;
    public string SoilTypeName {get;set;}=string.Empty;
    public WaterSourceType WaterSourceType {get;set;}
    public IrrigationMethod IrrigationMethod {get;set;}
    public WaterAvailability WaterAvailability {get;set;}
    public EnvironmentType EnvironmentType {get;set;}

}