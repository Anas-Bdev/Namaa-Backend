namespace Namaa.Application.Features.Consultations.Dtos;

public class ConsultationRequestDto
{
    public Guid Id { get; set; }
    public Guid FarmerId { get; set; }
    public string FarmerName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? Location { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<ExpertResponseDto> Responses { get; set; } = [];
}