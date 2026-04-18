
using System.Text.Json.Serialization;

namespace Namaa.Application.Features.Recommendations.Dtos;
public class CropRecommendationDto
{
     public CropDetailsDto Crop { get; set; } = default!;
    public int MatchPercentage { get; set; }

    [JsonIgnore] 
    public List<string> MatchingReasons { get; set; } = new();

    public string? Note { get; set; }

    [JsonIgnore] 
    public decimal MinExpectedProfitPerDonum { get; set; }

    [JsonIgnore] 
    public decimal MaxExpectedProfitPerDonum { get; set; }

    public decimal FinalEstimatedCostMin { get; set; }
    public decimal FinalEstimatedCostMax { get; set; }

    [JsonIgnore] 
    public decimal MinTotalCostPerDonum { get; set; }

    [JsonIgnore] 
    public decimal MaxTotalCostPerDonum { get; set; }

    public decimal RecommendedSeededAreaDonums { get; set; }
    public List<String> SuitableFarmingMethods { get; set; } = new();

    public decimal FinalEstimatedProfitMin { get; set; }
    public decimal FinalEstimatedProfitMax { get; set; }

    // Use this for UI / JSON output: average of min/max profit for the assigned land
    public decimal AverageEstimatedProfit { get; set; }

    public string AiSuitabilitySummary { get; set; } = string.Empty;
}