namespace Namaa.Application.Features.Consultations.Dtos;

public class ConsultationSummaryDto
{
    public Guid Id { get; set; }
    public Guid FarmerId { get; set; }
    public string FarmerName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int ResponseCount { get; set; }
}