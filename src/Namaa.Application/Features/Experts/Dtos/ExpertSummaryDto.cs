<<<<<<< HEAD
=======
using Namaa.Domain.Enums;
>>>>>>> dev-alaa

namespace Namaa.Application.Features.Experts.Dtos;
public class ExpertSummaryDto
{
   public Guid Id {get;set;}
   public string FullName {get;set;}=string.Empty;
   public string Specialization {get;set;}=string.Empty;
   public int GovernorateId {get;set;}
   public string Governorate {get;set;}=string.Empty;
   public int YearsOfExperience {get;set;}
}