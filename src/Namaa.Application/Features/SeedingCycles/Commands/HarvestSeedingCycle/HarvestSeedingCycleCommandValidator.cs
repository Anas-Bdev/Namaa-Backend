using FluentValidation;
using FluentValidation.Validators;

namespace Namaa.Application.Features.SeedingCycles.Commands.HarvestSeedingCycle;
public sealed class HarvestSeedingCycleCommandValidator : AbstractValidator<HarvestSeedingCycleCommand>
{
    public HarvestSeedingCycleCommandValidator()
    {
        RuleFor(x => x.ActualHarvestDate)
            .NotEmpty().WithMessage("Actual harvest date is required.");

        RuleFor(x => x.ActualYield)
            .GreaterThanOrEqualTo(0).WithMessage("Actual yield cannot be negative.");
    }
}