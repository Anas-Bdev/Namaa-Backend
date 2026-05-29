using FluentValidation;

namespace Namaa.Application.Features.SeedingCycles.Commands.UpdateSeedingCycle;
public class UpdateSeedingCycleCommandValidator : AbstractValidator<UpdateSeedingCycleCommand>
{
    public UpdateSeedingCycleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("The Seeding Cycle ID is required.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");    

        RuleFor(x => x.EstimatedHarvestDate)
            .NotEmpty().WithMessage("Estimated harvest date is required.");

        RuleFor(x => x.SeedQuantity)
            .GreaterThan(0).WithMessage("Seed quantity must be greater than zero.");

        RuleFor(x => x.SeedingArea)
            .GreaterThan(0).WithMessage("Seeding area must be greater than zero.");

        RuleFor(x => x.ExpectedYield)
            .GreaterThan(0).WithMessage("Expected yield must be greater than zero.");
    }
}