using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Domain.Land;
public sealed class Land : AuditableEntity
{
  public Guid FarmerId {get;}
  public int CityId {get;private set;}
  public int SoilId {get;private set;}
  public string? Name {get;private set;}
  public double Area {get; private set;}
  public WaterSourceType WaterSourceType {get;private set;}
  public WaterAvailability WaterAvailability {get;private set;}
  public EnvironmentType EnvironmentType {get;private set;}
  
    private Land() {}
    
    private Land(Guid id,Guid farmerId,int cityId,int soilId,string name,double area,WaterSourceType waterSourceType,WaterAvailability waterAvailability,EnvironmentType environmentType)
    :base(id)
  {
    FarmerId=farmerId;
    CityId=cityId;
    SoilId=soilId;
    Name=name;
    Area=area;
    WaterSourceType=waterSourceType;
    WaterAvailability=waterAvailability;
    EnvironmentType=environmentType;
  }
  public static Result<Land> Create(Guid id,Guid farmerId,int cityId,int soilId,string name,double area,WaterSourceType waterSourceType,WaterAvailability waterAvailability,EnvironmentType environmentType)
  {
    if(id==Guid.Empty)
    return LandErrors.IdRequired;

    if(farmerId==Guid.Empty)
    return LandErrors.FarmerIdRequired;

    if(cityId<=0)
    return LandErrors.CityRequired;
    
    if(soilId<=0)
    return LandErrors.SoilRequired;

    if(string.IsNullOrWhiteSpace(name))
    return LandErrors.NameRequired;

    if(area<=0)
    return LandErrors.AreaInvalid;

    return new Land(id,farmerId,cityId,soilId,name.Trim(),area,waterSourceType,waterAvailability,environmentType);
    
  }
 
public Result<Updated> Update(int cityId,int soilId,string name,double area,WaterSourceType waterSourceType,WaterAvailability waterAvailability,EnvironmentType environmentType)
  {

    if(cityId<=0)
    return LandErrors.CityRequired;
    
    if(soilId<=0)
    return LandErrors.SoilRequired;

    if(string.IsNullOrWhiteSpace(name))
    return LandErrors.NameRequired;

    if(area<=0)
    return LandErrors.AreaInvalid;

    CityId=cityId;
    SoilId=soilId;
    Name=name;
    Area=area;
    WaterSourceType=waterSourceType;
    WaterAvailability=waterAvailability;
    EnvironmentType=environmentType;
   
   return Result.Updated;
  }
}