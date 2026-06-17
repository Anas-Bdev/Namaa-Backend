namespace Namaa.Application.Features.Consultations.Dtos;
public class ConsultationDetailsDto
{
    public Guid Id { get; set; }
    public Guid FarmerId { get; set; }
    
    public Guid? ExpertId { get; set; } 
    
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    
    public List<MessageDto> Messages { get; set; } = [];
}