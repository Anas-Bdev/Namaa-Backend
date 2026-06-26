using Namaa.Domain.Enums;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Application.Features.SeedingCycles.Dtos;
public class SeedingCycleDto
{
    public Guid SeedingCycleId {get;set;}
    public Guid LandId {get;set;}
    public string CropName {get;set;}=string.Empty;
    public DateTime StartDate {get;set;}
    public DateTime EstimatedHarvestDate {get;set;}
    public DateTime? ActualHarvestDate {get;set;}
    public string Status {get;set;}=string.Empty;
    public  double SeedQuantity {get;set;}
    public double SeedingArea {get;set;}
    public double ExpectedYield {get;set;}
    public double? ActualYield {get;set;}
    public string EnvironmentType {get;set;}=string.Empty;
}