using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Experts.Dtos;
public class ExpertSummaryDto
{
   public Guid Id {get;set;}
   public string FullName {get;set;}=string.Empty;
   public string Specialization {get;set;}=string.Empty;
   public int CityId {get;set;}
   public int YearsOfExperience {get;set;}
}