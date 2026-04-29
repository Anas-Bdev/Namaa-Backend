using System.ClientModel;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Recommendations.Dtos;
using Namaa.Application.Features.Recommendations.Queries;

namespace Namaa.Infrastructure.Services;

using System.Text.Json;
using OpenAI.Chat;
using Microsoft.Extensions.Configuration;
using Namaa.Application.Common.Models;

public class OpenAiConsultantService(IConfiguration configuration) : IAiConsultantService
{
  
    private readonly string _apiKey = configuration["OpenAi:ApiKey"]!; 

    public async Task<CropRecommendationAiResult> GenerateFarmerAdviceAsync(GetCropRecommendationQuery request, List<CropRecommendationDto> topCrops, CancellationToken cancellationToken)
    {
        var chatClient = new ChatClient("gpt-5.4-mini", _apiKey); 
        
        string prompt = BuildSystemPrompt(request, topCrops);

        var options = new ChatCompletionOptions { ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat() };
        var messages = new List<ChatMessage> { new UserChatMessage(prompt) };

        ClientResult<ChatCompletion> result = await chatClient.CompleteChatAsync(messages, options, cancellationToken);
        
        return ParseAiResponse(result.Value.Content[0].Text);
    }

    private string BuildSystemPrompt(GetCropRecommendationQuery request, List<CropRecommendationDto> topCrops)
    {
        string cityName = request.GovernorateId switch
        {
            1 => "Jerusalem", 2 => "Ramallah", 3 => "Hebron", 4 => "Bethlehem",
            5 => "Nablus", 6 => "Jenin", 7 => "Tulkarm", 8 => "Qalqilya",
            9 => "Salfit", 10 => "Tubas", 11 => "Jericho", 12 => "Gaza",
            _ => throw new System.Diagnostics.UnreachableException()
        };

        string soilName = request.SoilTypeId switch
        {
            1 => "Red Soil",
            2 => "Rendzina Soil",
            3 => "Clay Soil",
            4 => "Sandy Soil",
            _ => throw new System.Diagnostics.UnreachableException()
        };

        string farmerContext = $@"
FARMER PROFILE:
- Location: {cityName}, Palestine
- Total Land Area: {request.LandAreaDonums} Donums
- Soil Type: {soilName} 
";

        var cropSummary = string.Join("\n", topCrops.Select(c =>
              $"- ID: {c.Crop.Id} | Name: {c.Crop.Name} | Season: {c.Crop.Season} | Match Score: {c.MatchPercentage}%\n" +
              $"  Technical Reasons: {string.Join(", ", c.MatchingReasons)}\n"));

        return $@"
# STEP 1: DYNAMIC 
Attempt to find:
1. 'Palestinian Ministry of Agriculture (MoA) crop reports March 2026'
2. '{cityName} historical and current market price trends, supply/demand, and oversupply warnings 2026'

# FALLBACK RULE:
If live data is unavailable, proceed as a '2026 Strategic Simulator' using:
- Palestinian historical crop yield patterns
- MoA crop recommendations and guidelines
- Regional planting trends
- City-level market saturation and planting history
- Seasonal planting and harvest history

---

# STEP 2: CONSULTANCY PERSONA
You are the Lead Agricultural Strategist for NAMA'A.

{farmerContext}

AGRICULTURAL DATA INPUT:
{cropSummary}

---

# STEP 3: ENVIRONMENT & TECHNICAL MATCH
For EACH crop:

- Provide an array 'suitableFarmingMethods' including all methods the farmer can use safely (e.g., Greenhouse adaptation possible for open-field crops).
- Highlight any environmental limitations and note if yields may vary due to adaptation.
- Technical suggestions are optional; if provided, they must be practical, actionable, and quantifiable.

---

# STEP 4: AI SUITABILITY SUMMARY
For EACH crop:

- Provide ONE 'AiSuitabilitySummary' (2–3 sentences max)
- Explain soil, water, and environment suitability using the Farmer Profile above.
- Highlight main limitation if Match < 100%
- Do NOT repeat TechnicalSuggestion

---

# STEP 5: DYNAMIC SEEDING AREA ALGORITHM (STRICTLY ENFORCED)
You must calculate 'SuggestedLandDistributions' for EACH crop independently using this exact 3-step dynamic algorithm:

1. MARKET & SEASONAL RISK ASSESSMENT: First, analyze the crop's current season and the local market conditions (oversupply danger vs. high demand). Based on this, set a 'Dynamic Max Limit' out of the farmer's total {request.LandAreaDonums} Donums:
   - NO DANGER / HIGH DEMAND: If it is the peak season and the market is safe, allow up to 80% of total land for Field Crops, or 50% for Vegetables/Greens.
   - HEAVILY SEEDED / OVERSUPPLY RISK: If the market is flooded or the season is ending, heavily restrict the crop to protect the farmer. Max 15% to 20% of total land.
   - NEUTRAL: Use standard baseline caps (70% Field Crops, 40% Vegetables, 20% Greens).

2. SCORE ADJUSTMENT: Multiply the 'Dynamic Max Limit' (from Step 1) by the crop's Match Score percentage (e.g., 85% = 0.85). 

3. THE HARD FLOOR RULE: The absolute minimum allocation for ANY crop in the top choices is 0.5 Donums. If your final calculation is less than 0.5, you MUST round it up to 0.5. You are strictly forbidden from outputting 0.

Round final numbers to the nearest 0.5 (e.g., 0.5, 1.0, 1.5, 2.0).
---

# STEP 6: PROFIT & YIELD CALCULATION (FOR AI REFERENCE)
- Use average yield and average price per kg
- Apply conservative risk factor for min yield/profit
- Do NOT let unusually high price gaps inflate recommended area

---

# STEP 7: OUTPUT REQUIREMENTS
- JSON ONLY. No extra text.
- Use Crop.Id exactly as provided
- Include:
{{
   ""SuggestedLandDistributions"": {{ ""CropId"": float }},
   ""AiSuitabilitySummaries"": {{ ""CropId"": ""string 2–3 sentence explanation"" }}
}}
";
 }

    private CropRecommendationAiResult ParseAiResponse(string jsonContent)
    {
        var distributions = new Dictionary<int, decimal>();
        var summaries = new Dictionary<int, string>();

        using JsonDocument doc = JsonDocument.Parse(jsonContent);
        var root = doc.RootElement;

        if (root.TryGetProperty("SuggestedLandDistributions", out var distElem))
        {
            foreach (var property in distElem.EnumerateObject())
            {
                if (int.TryParse(property.Name, out int id))
                {
                    if (property.Value.TryGetDecimal(out decimal area))
                    {
                        distributions[id] = area;
                    }
                }
            }
        }

        if (root.TryGetProperty("AiSuitabilitySummaries", out var sumElem))
        {
            foreach (var property in sumElem.EnumerateObject())
            {
                if (int.TryParse(property.Name, out int id))
                {
                    summaries[id] = property.Value.GetString() ?? "This crop is highly suitable for your land.";
                }
            }
        }

        return new CropRecommendationAiResult
        {
            SuggestedLandDistributions = distributions,
            AiSuitabilitySummaries = summaries
        };
    }
}