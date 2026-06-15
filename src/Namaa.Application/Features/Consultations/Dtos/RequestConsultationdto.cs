using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Consultations.Dtos;
public class RequestConsultationDto
{
    public Guid Id {get;set;}
    public Guid FarmerId {get;set;}
    public ConsultationStatus Status {get;set;}
    public string Title {get;set;}=string.Empty;
    public string Description {get;set;}=string.Empty;
    public string? ImageUrl {get;set;}
}