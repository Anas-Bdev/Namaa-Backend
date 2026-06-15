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

    public async Task<string> GeneratePrimaryAdviceAsync(string title, string description, string? imageUrl, CancellationToken cancellationToken)
    {
        var chatClient = new ChatClient("gpt-5.4-mini", _apiKey);

        string systemPrompt = @"
You are a Senior Agricultural Expert for the NAMA'A platform in Palestine. 
A farmer has submitted a consultation request seeking primary advice before a human expert reviews the ticket.

Your objective:
- Analyze the title and description provided by the farmer.
- If an image is provided, carefully inspect the plant leaves, soil, or fruit for visible symptoms of disease, pests, or nutrient deficiency.
- Provide immediate, practical, and safe first-aid agricultural advice.
- Keep the tone encouraging, professional, and accessible.
- Format the response using clean Markdown with bullet points for readability. Do NOT use JSON.";

    var messages = new List<ChatMessage>
    {
        new SystemChatMessage(systemPrompt)
    };

    var userContentParts = new List<ChatMessageContentPart>
    {
        ChatMessageContentPart.CreateTextPart($"**Title:** {title}\n**Description:** {description}")
    };

    if (!string.IsNullOrWhiteSpace(imageUrl))
    {
        userContentParts.Add(ChatMessageContentPart.CreateImagePart(new Uri(imageUrl)));
    }

    messages.Add(new UserChatMessage(userContentParts));

    var options = new ChatCompletionOptions();

    ClientResult<ChatCompletion> result = await chatClient.CompleteChatAsync(messages, options, cancellationToken);

    return result.Value.Content[0].Text;
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

        // FIX 2: Restored the cropSummary generation logic so the prompt has the actual data
        var cropSummary = string.Join("\n", topCrops.Select(c =>
            $"- ID: {c.Crop.Id} | Name: {c.Crop.Name} | Season: {c.Crop.Season} | Match Score: {c.MatchPercentage}%\n" +
            $"  Technical Reasons: {string.Join(", ", c.MatchingReasons)}\n"));

        return $@"
# SYSTEM ROLE
You are the Lead Agricultural Strategist for the NAMA'A agricultural platform. 
Your core function is to act as an Expert Reasoning Engine. The backend system has already filtered out incompatible crops using strict water and environmental constraints. You will receive a pre-approved list of crops.

{farmerContext}

# PRE-VALIDATED CROP DATA:
{cropSummary}

---

# STEP 1: AI SUITABILITY SUMMARY (EXPLAINABILITY)
For EACH crop provided above:
- Write ONE 'AiSuitabilitySummary' (2–3 sentences max).
- Explain exactly why this crop works for the Farmer's specific location ({cityName}) and soil type ({soilName}).
- This serves as the Explainable AI (XAI) output so the farmer trusts the recommendation.

---

# STEP 2: DYNAMIC SEEDING AREA ALGORITHM (STRICTLY ENFORCED)
You must calculate 'SuggestedLandDistributions' for EACH crop using this exact deterministic logic based on the farmer's total {request.LandAreaDonums} Donums:

1. MARKET RISK ASSESSMENT (Based on your internal knowledge of Palestinian agriculture):
   - SAFE/STAPLE CROP: Allow up to 80% of total land for Field Crops, or 50% for Vegetables.
   - HIGH YIELD/OVERSUPPLY RISK: Cap at 15% to 20% of total land to protect the farmer from market saturation.
2. MATCH SCORE WEIGHTING: Multiply the capped limit by the crop's Match Score (e.g., 85% Match = multiply by 0.85). 
3. HARD FLOOR RULE: The absolute minimum allocation for ANY crop is 0.5 Donums. You must round up if the result is lower.
4. Round final numbers to the nearest 0.5.

---

# STEP 3: OUTPUT REQUIREMENTS
- STRICT JSON FORMAT ONLY. No markdown formatting outside the JSON block.
- Use the integer CropId exactly as provided.
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