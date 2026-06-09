using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Recommendations.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.ReferenceData;

namespace Namaa.Application.Features.Recommendations.Queries;

public class GetCropRecommendationQueryHandler(IAppDbContext context, IAiConsultantService aiConsultantService) 
    : IRequestHandler<GetCropRecommendationQuery, Result<List<CropRecommendationDto>>>
{
    // Static mapping for compatible soils
    private static readonly Dictionary<int, List<int>> CompatibleSoils = new()
    {
        { 1, new List<int> { 2, 3 } },
        { 2, new List<int> { 1 } },
        { 3, new List<int> { 1 } },
        { 4, new List<int>() }
    };

    public async Task<Result<List<CropRecommendationDto>>> Handle(GetCropRecommendationQuery request, CancellationToken cancellationToken)
    {
        // 1. FAIL FAST: Validate Soil Type
        bool isValidSoil = await context.SoilTypes.AnyAsync(s => s.Id == request.SoilTypeId, cancellationToken);
        if (!isValidSoil)
        {
            return ApplicationErrors.InvalidSoilType;
        }

        // 2. FETCH DATA: Get crops for the specific Governorate
        var cityCrops = await context.Crops
            .Include(c => c.SuitableSoilTypes)
            .Include(c => c.Governorate)
            .Where(c => c.GovernorateId == request.GovernorateId)
            .ToListAsync(cancellationToken);

        if (!cityCrops.Any())
        {
            return ApplicationErrors.RegionCropsNotFound;
        }

        // 3. SCORE: Evaluate all crops against the farmer's profile
        var scoredRecommendations = new List<CropRecommendationDto>();
        foreach (var crop in cityCrops)
        {
            var recommendation = CalculateConfidenceScore(crop, request);

            if (recommendation.MatchPercentage >= 40)
            {
                scoredRecommendations.Add(recommendation);
            }
        }

        // 4. FILTER: Take only the top 3 best-matching crops
        var top3Crops = scoredRecommendations
            .OrderByDescending(c => c.MatchPercentage)
            .Take(3)
            .ToList();

        // 5. AI INTEGRATION: Call the AI Service for dynamic land distribution and summaries
        var aiResult = await aiConsultantService.GenerateFarmerAdviceAsync(request, top3Crops, cancellationToken);

        // 6. MAP AI RESULTS: Apply AI calculations back to the DTOs
        foreach (var cropDto in top3Crops)
        {
            if (aiResult.SuggestedLandDistributions.TryGetValue(cropDto.Crop.Id, out decimal assignedDonums))
            {
                cropDto.RecommendedSeededAreaDonums = Math.Round(assignedDonums, 2);
                
                // Calculate final financials based on the AI's assigned Donums
                cropDto.FinalEstimatedProfitMax = cropDto.RecommendedSeededAreaDonums * cropDto.MaxExpectedProfitPerDonum;
                cropDto.FinalEstimatedProfitMin = cropDto.RecommendedSeededAreaDonums * cropDto.MinExpectedProfitPerDonum;
                cropDto.FinalEstimatedCostMin = cropDto.RecommendedSeededAreaDonums * cropDto.MinTotalCostPerDonum;
                cropDto.FinalEstimatedCostMax = cropDto.RecommendedSeededAreaDonums * cropDto.MaxTotalCostPerDonum;
                cropDto.AverageEstimatedProfit = (cropDto.FinalEstimatedProfitMax + cropDto.FinalEstimatedProfitMin) / 2;
            }

            if (aiResult.AiSuitabilitySummaries.TryGetValue(cropDto.Crop.Id, out string? summary))
            {
                cropDto.AiSuitabilitySummary = summary;
            }
        }

        // 7. RETURN: Send the final payload
        return top3Crops;
    }

    private CropRecommendationDto CalculateConfidenceScore(Crop crop, GetCropRecommendationQuery request)
    {
        int score = 0;
        var reasons = new List<string>();
        string? note = null;

        const int SoilWeight = 30;
        const int EnvironmentWeight = 25;
        const int WaterWeight = 25;
        const int IrrigationWeight = 20;

        // ==========================================
        // A. SOIL LOGIC
        // ==========================================
        bool isExactMatch = crop.SuitableSoilTypes.Any(s => s.Id == request.SoilTypeId);

        if (isExactMatch)
        {
            score += SoilWeight;
            reasons.Add("Soil: Perfect match for this crop.");
        }
        else if (crop.SuitableSoilTypes.Any(s => CompatibleSoils.ContainsKey(request.SoilTypeId) && CompatibleSoils[request.SoilTypeId].Contains(s.Id)))
        {
            score += 15;
            reasons.Add("Soil Mismatch: Acceptable match, but will require minor soil amendments like Peat Moss or Compost.");
        }
        else
        {
            reasons.Add("Soil Mismatch: Severe mismatch. Requires significant soil amendment and heavy fertilization.");
        }

        // ==========================================
        // B. ENVIRONMENT LOGIC
        // ==========================================
        string cropMethod = crop.FarmingMethod!.Replace(" ", "").ToLower();
        string farmerEnv = request.EnvironmentType.ToString().ToLower();
        var recommendedMethod = new List<string> { crop.FarmingMethod };

        if (farmerEnv == "greenhouse" && crop.Category == "Field Crops")
        {
            score -= 100;
            reasons.Add("Mismatch: Field crops are not suitable for greenhouse cultivation.");
        }
        else if (cropMethod == farmerEnv)
        {
            score += EnvironmentWeight;
            reasons.Add($"Environment: Perfect match for {request.EnvironmentType} farming.");
        }
        else if (farmerEnv == "greenhouse" && cropMethod == "openfield")
        {
            if (crop.SupportedEnvironmentTypes.Any(e => e.ToLower().Contains("greenhouse")))
            {
                score += 15; 
                recommendedMethod.Add("greenhouse");
                reasons.Add("Environment: Open-field variety can adapt to greenhouse.");
                note = "This crop is originally open-field, but greenhouse adaptation is possible. Yields may vary; consult MoA guidelines for precise scheduling.";
            }
            else
            {
                reasons.Add("Environment: Not recommended for greenhouse cultivation.");
            }
        }
        else if (farmerEnv == "openfield" && cropMethod.Contains("greenhouse"))
        {
            score -= 100;
            reasons.Add("Mismatch: This crop requires a greenhouse and cannot be grown in open field.");
        }
        else
        {
            reasons.Add("Environment: Compatibility uncertain; crop may not thrive in current conditions.");
        }

        // ==========================================
        // C. WATER AVAILABILITY LOGIC
        // ==========================================
        string cropWaterNeed = crop.WaterRequirementCategory!.ToLower();
        WaterAvailability farmerWater = request.WaterAvailability;
        int cityRain = crop.Governorate?.AvgRainfall ?? 0;
        string cropSeason = crop.Season!.ToLower();

        bool isRainySeason = cropSeason.Contains("winter") || cropSeason.Contains("spring");
        bool isHighRainRegion = cityRain >= 500;
        int waterScore = 0;

        if (cropWaterNeed.Contains("high"))
        {
            if (farmerWater == WaterAvailability.Reliable) waterScore = WaterWeight;
            else if (farmerWater == WaterAvailability.Intermittent) waterScore = 10;
        }
        else if (cropWaterNeed.Contains("medium"))
        {
            if (farmerWater == WaterAvailability.Reliable || farmerWater == WaterAvailability.Intermittent) waterScore = WaterWeight;
            else if (farmerWater == WaterAvailability.Seasonal && isRainySeason && isHighRainRegion) waterScore = 20;
        }
        else
        {
            waterScore = WaterWeight;
        }

        score += waterScore;

        switch (waterScore)
        {
            case WaterWeight:
                if (farmerWater == WaterAvailability.Seasonal)
                    reasons.Add("Water: Ideal low-water crop for rain-fed seasonal farming.");
                else
                    reasons.Add("Water: Sufficient supply available for this crop.");
                break;
            case 20:
                reasons.Add($"Water: Seasonal supply supported by {cityRain}mm of regional winter rainfall.");
                break;
            case 10:
                reasons.Add("Water: Intermittent supply. You will need water storage tanks for dry days.");
                break;
            case 0:
                reasons.Add($"Risk: Your {farmerWater} water supply is insufficient for {crop.Name}.");
                break;
        }

        // ==========================================
        // D. IRRIGATION METHOD LOGIC
        // ==========================================
        string farmerIrrigation = request.IrrigationMethod.ToString();
        bool isIrrigationMatch = crop.SupportedIrrigationMethods
            .Any(m => m.Equals(farmerIrrigation, StringComparison.OrdinalIgnoreCase));

        int irrigationScore = 0;

        switch (isIrrigationMatch)
        {
            case true:
                irrigationScore = IrrigationWeight;
                reasons.Add($"Irrigation: {request.IrrigationMethod} is ideal.");
                break;
            case false when request.IrrigationMethod == IrrigationMethod.Surface && crop.Category == "Field Crops":
                irrigationScore = 10;
                reasons.Add("Irrigation Risk: Surface irrigation acceptable but less efficient.");
                break;
            case false:
                reasons.Add($"Irrigation Mismatch: {request.IrrigationMethod} is not optimized for this crop.");
                break;
        }

        score += irrigationScore;

        // ==========================================
        // E. FINANCIAL CALCULATIONS
        // ==========================================
        decimal minYieldKgPerDonum = (decimal)crop.MinProductionPerDonum * 1000m;
        decimal maxYieldKgPerDonum = (decimal)crop.MaxProductionPerDonum * 1000m;

        decimal avgYieldKgPerDonum = (minYieldKgPerDonum + maxYieldKgPerDonum) / 2m;
        decimal avgPricePerKg = (crop.MinExpectedPrice + crop.MaxExpectedPrice) / 2m;

        decimal calculatedMinCost = crop.MinEstimatedCost;
        decimal calculatedMaxCost = crop.MaxEstimatedCost;

        decimal minProfitPerDonum = Math.Max(0, (minYieldKgPerDonum * crop.MinExpectedPrice) - calculatedMaxCost);
        decimal maxProfitPerDonum = (avgYieldKgPerDonum * avgPricePerKg) - calculatedMinCost;
        decimal averageProfitPerDonum = (minProfitPerDonum + maxProfitPerDonum) / 2m;

        // ==========================================
        // F. RETURN DTO
        // ==========================================
        return new CropRecommendationDto
        {
            Crop = new CropDetailsDto
            {
                Id = crop.Id,
                Name = crop.Name!,
                ImageUrl = crop.ImageUrl!,
                Category = crop.Category!,
                Season = crop.Season!,
                PlantingTime = crop.PlantingTime!,
                HarvestTime = crop.HarvestTime!,
                DaysToHarvest = crop.DaysToHarvest
            },
            MatchPercentage = Math.Min(95, score),
            MinTotalCostPerDonum = calculatedMinCost,
            MaxTotalCostPerDonum = calculatedMaxCost,
            MinExpectedProfitPerDonum = minProfitPerDonum,
            MaxExpectedProfitPerDonum = maxProfitPerDonum,
            AverageEstimatedProfit = averageProfitPerDonum,
            SuitableFarmingMethods = recommendedMethod,
            Note = note,
            MatchingReasons = reasons
        };
    }
}