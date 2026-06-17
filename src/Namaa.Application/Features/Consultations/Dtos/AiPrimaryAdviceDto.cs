namespace Namaa.Application.Features.Consultations.Dtos;
public class AiPrimaryAdviceDto
{
    public string Diagnosis { get; init; } = string.Empty;
    
    public List<string> ActionPlan { get; init; } = [];
    
    public string TreatmentRecommendations { get; init; } = string.Empty;
    
    public string PreventativeMeasures { get; init; } = string.Empty;
    
    public string UrgencyLevel { get; init; } = string.Empty;
}