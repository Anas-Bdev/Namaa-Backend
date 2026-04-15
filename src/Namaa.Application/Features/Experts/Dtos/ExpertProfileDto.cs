
namespace Namaa.Application.Features.Experts.Dtos;
public class ExpertProfileDto
{
    public Guid Id { get; set; }
    public string? Specialization { get; set; }=string.Empty;
    public int? YearsOfExperience { get; set; }
    public int? GovernorateId { get; set; }
    public string? Governorate {get;set;}=string.Empty;
    public string? AddressDetail { get; set; }=string.Empty;
    public bool? CanVisitOnSite { get; set; }
    public string CvUrl { get; set; } = string.Empty;
    public List<ExpertAvailabilityDto> Availabilities { get; set; } = [];

}