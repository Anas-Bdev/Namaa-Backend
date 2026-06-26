using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Lands.Dtos;
public class LandDto
{
    public string AddressDetail {get;set;}=string.Empty;
    public Guid LandId {get;set;}
    public string Name {get;set;}=string.Empty;
    public double AreaDonum {get;set;}
    public int GovernorateId {get;set;}
    public int SoilTypeId {get;set;}
    public string GovernorateName {get;set;}=string.Empty;
    public string SoilTypeName {get;set;}=string.Empty;
    public string WaterSourceType {get;set;}=string.Empty;
    public string WaterAvailability {get;set;}=string.Empty;

}