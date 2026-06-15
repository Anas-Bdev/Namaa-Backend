using Namaa.Application.Common.Models;
using Namaa.Application.Features.Recommendations.Dtos;
using Namaa.Application.Features.Recommendations.Queries;

namespace Namaa.Application.Common.Interfaces;
public interface IAiConsultantService
{
    Task<CropRecommendationAiResult> GenerateFarmerAdviceAsync(GetCropRecommendationQuery request,List<CropRecommendationDto> topCrops,CancellationToken cancellationToken);
    Task<string> GeneratePrimaryAdviceAsync(string title,string description,string? imageUrl,CancellationToken cancellationToken);
}

