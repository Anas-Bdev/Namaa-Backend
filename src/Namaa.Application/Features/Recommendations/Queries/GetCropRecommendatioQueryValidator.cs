using FluentValidation;

namespace Namaa.Application.Features.Recommendations.Queries;
public sealed class GetCropRecommendationQueryValidator : AbstractValidator<GetCropRecommendationQuery>
{
    public GetCropRecommendationQueryValidator()
    {
    RuleFor(x => x.GovernorateId)
            .NotEmpty().WithMessage("Governorate is required.")
            .GreaterThan(0).WithMessage("Invalid Governorate ID.");

        RuleFor(x => x.SoilTypeId)
            .NotEmpty().WithMessage("Soil type is required.")
            .GreaterThan(0).WithMessage("Invalid Soil Type ID.");

        // 2. The "NAMA'A" Business Logic (The 0.5 Donum Rule)
        RuleFor(x => x.LandAreaDonums)
            .GreaterThanOrEqualTo(0.5)
            .WithMessage("Recommendations require at least 0.5 donums of land to be accurate.");

        // 3. Enum Safety (Prevents "99" or invalid integers)
        RuleFor(x => x.WaterAvailability).IsInEnum();
        RuleFor(x => x.EnvironmentType).IsInEnum();
        RuleFor(x => x.IrrigationMethod).IsInEnum();
    }
}