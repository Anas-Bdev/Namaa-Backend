using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Experts.Dtos;
public class ExpertProfileDto
{
    public Guid Id { get; set; }
    public string FullName {get;set;}=string.Empty;
    public string Specialization { get; set; }=string.Empty;
    public int YearsOfExperience { get; set; }
    public int CityId { get; set; }
    public string AddressDetail { get; set; }=string.Empty;
    public bool CanVisitOnSite { get; set; }
    public string CvUrl { get; set; } = string.Empty;
    public List<ExpertAvailabilityDto> Availabilities { get; set; } = [];

}