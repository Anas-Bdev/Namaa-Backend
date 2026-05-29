namespace Namaa.Application.Features.Consultations.Dtos;

public class ExpertResponseDto
{
    public Guid Id { get; set; }
    public Guid ExpertId { get; set; }
    public string ExpertName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}