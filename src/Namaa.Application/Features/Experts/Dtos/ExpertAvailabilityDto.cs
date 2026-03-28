using System.ComponentModel.DataAnnotations;

namespace Namaa.Application.Features.Experts.Dtos;
public class ExpertAvailabilityDto
{
    public string Day {get;set;}=string.Empty;
    public TimeSpan StartTime {get;set;}
    public TimeSpan EndTime {get;set;}
}