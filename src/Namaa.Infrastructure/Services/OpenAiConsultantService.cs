using System.ClientModel;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Recommendations.Dtos;
using Namaa.Application.Features.Recommendations.Queries;

namespace Namaa.Infrastructure.Services;

using System.Text.Json;
using OpenAI.Chat;
using Microsoft.Extensions.Configuration;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Consultations.Dtos;

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

    public async Task<AiPrimaryAdviceDto> GeneratePrimaryAdviceAsync(string title, string description, string? imageUrl, CancellationToken cancellationToken)
    {
        var chatClient = new ChatClient("gpt-5.4-mini", _apiKey);
string systemPrompt = @"
You are a Senior Agricultural Expert for the NAMA'A platform in Palestine. 
A farmer has submitted a consultation request seeking primary advice before a human expert reviews the ticket.

Your objective:
- Analyze the title and description provided by the farmer.
- If an image is provided, carefully inspect the plant leaves, soil, or fruit for visible symptoms of disease, pests, or nutrient deficiency.
- Provide immediate, practical, and safe first-aid agricultural advice.

IMAGE VALIDATION GUARDRAIL (CRITICAL):
If an image is provided but it is clearly NOT related to agriculture, plants, crops, pests, or farming (such as a selfie, a car, furniture, or generic text), you MUST reject the consultation request by returning this exact structured JSON response:
{
    ""Diagnosis"": ""The uploaded image does not appear to be related to agriculture or plants. Please submit a clear photo of your affected crop."",
    ""ActionPlan"": [""Take a new photo focusing closely on the damage to leaves, stems, or fruit."", ""Make sure the lighting is clear and resubmit your request.""],
    ""TreatmentRecommendations"": ""N/A"",
    ""PreventativeMeasures"": ""N/A"",
    ""UrgencyLevel"": ""Low""
}

TONE & LANGUAGE RULES (CRITICAL):
1. Speak directly to the farmer using simple, everyday language. 
2. DO NOT use academic or complex scientific terms (e.g., use 'tomatoes or eggplants' instead of 'solanaceous').
3. Keep the 'Diagnosis' to exactly one short, confident sentence.
4. Keep the 'TreatmentRecommendations' punchy and easy to read. If unsure, provide safe, general first-aid treatment rather than overwhelming the farmer with 10 different chemical options.

CRITICAL INSTRUCTION: You MUST respond ONLY with a valid JSON object. Do not include any markdown formatting like ```json or conversational text. 
The JSON object MUST strictly match the following keys exactly:
{
    ""Diagnosis"": ""<A brief, clear issue of summary the>"",
    ""ActionPlan"": [""<Step 1>"", ""<Step 2>""],
    ""TreatmentRecommendations"": ""<Specific biological, chemical, or organic treatments>"",
    ""PreventativeMeasures"": ""<Long-term advice prevent recurrence to>"",
    ""UrgencyLevel"": ""<Must Critical High, Low, Medium, be of: one>""
}";
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

    var options = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat()
        };

    ClientResult<ChatCompletion> result = await chatClient.CompleteChatAsync(messages, options, cancellationToken);

    string jsonResponse = result.Value.Content[0].Text;

    var optionsJson = new JsonSerializerOptions 
        { 
            PropertyNameCaseInsensitive = true 
        };

    var dto = JsonSerializer.Deserialize<AiPrimaryAdviceDto>(jsonResponse, optionsJson);

    if (dto is null)
        {
            throw new InvalidOperationException("The AI Service failed to return a valid structured response.");
        }

    return dto;
    
    }

    public async Task<CropThresholds> GetCropTemperatureLimitsAsync(string cropName, CancellationToken cancellationToken)
    {
        var chatClient = new ChatClient("gpt-5.4-mini", _apiKey);

    string prompt = $@"
        You are a botanical expert for the NAMA'A agricultural platform.
        Provide the safe minimum and maximum temperature limits in Celsius for growing {cropName}.
        
        Return ONLY a valid JSON object in this exact format:
        {{
            ""minTemp"": 0.0,
            ""maxTemp"": 0.0
        }}
        Do not include any markdown, explanations, or additional text.";

    var messages = new List<ChatMessage> { new UserChatMessage(prompt) };
    
    var options = new ChatCompletionOptions 
    { 
        ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat() 
    };

    ClientResult<ChatCompletion> result = await chatClient.CompleteChatAsync(messages, options, cancellationToken);
    
    string jsonResponse = result.Value.Content[0].Text;

    var optionsJson = new JsonSerializerOptions 
    { 
        PropertyNameCaseInsensitive = true 
    };

    var thresholds = JsonSerializer.Deserialize<CropThresholds>(jsonResponse, optionsJson);

    return thresholds ?? new CropThresholds
    {
        MaxTemp=40.0m,
        MinTemp=0.0m
    };
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