
namespace Namaa.Application.Features.Experts.Dtos;
public class ExpertListItemDto
{
   public Guid Id {get;set;}
   public string FullName {get;set;}=string.Empty;
   public string Specialization {get;set;}=string.Empty;
   public string Governorate {get;set;}=string.Empty;
   public int YearsOfExperience {get;set;}
   public string? ProfileImageUrl {get;set;}
   public bool CanVisitOnSite {get;set;}
}