namespace Namaa.Application.Common.Models;
public class CropRecommendationAiResult
{
    public Dictionary<int, decimal> SuggestedLandDistributions { get; set; } = new();
    public Dictionary<int, string> AiSuitabilitySummaries { get; set; } = new();
}