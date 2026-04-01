using MediatR;
using Namaa.Application.Features.Recommendations.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Recommendations.Queries;
public sealed record GetCropRecommendationQuery(
        int GovernorateId,
        int SoilTypeId,
        double LandAreaDonums,
        WaterAvailability WaterAvailability,
        EnvironmentType EnvironmentType,
        IrrigationMethod IrrigationMethod
    ) : IRequest<Result<List<CropRecommendationDto>>>;