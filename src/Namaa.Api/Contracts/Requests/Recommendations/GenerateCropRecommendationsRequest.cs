using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Recommendations.Queries;
using Namaa.Domain.Enums;

namespace Namaa.Api.Contracts.Requests.Recommendations;
public class GenerateCropRecommendationRequest
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid governorate.")]
    public int GovernorateId { get; init; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid soil type.")]
    public int SoilTypeId { get; init; }

    [Required]
    [Range(0.5, 10000, ErrorMessage = "Land area must be at least 0.5 donums.")]
    public double LandAreaDonums { get; init; }

    [Required]
    [EnumDataType(typeof(WaterAvailability))]
    public WaterAvailability WaterAvailability { get; init; }

    [Required]
    [EnumDataType(typeof(EnvironmentType))]
    public EnvironmentType EnvironmentType { get; init; }

    [Required]
    [EnumDataType(typeof(IrrigationMethod))]
    public IrrigationMethod IrrigationMethod { get; init; }

   public GetCropRecommendationQuery ToQuery()
    {
        return new(GovernorateId,SoilTypeId,LandAreaDonums,WaterAvailability,EnvironmentType,IrrigationMethod);
    }
}