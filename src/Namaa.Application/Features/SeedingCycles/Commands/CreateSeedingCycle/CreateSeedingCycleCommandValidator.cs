using FluentValidation;
namespace Namaa.Application.Features.SeedingCycles.Commands.CreateSeedingCycle;
public sealed class CreateSeedingCycleCommandValidator : AbstractValidator<CreateSeedingCycleCommand>
{
    public CreateSeedingCycleCommandValidator(){

       RuleFor(x => x.LandId)
            .NotEmpty().WithMessage("Land ID is required and cannot be empty.");


        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EstimatedHarvestDate)
            .NotEmpty().WithMessage("Estimated harvest date is required.")
            .GreaterThan(x => x.StartDate).WithMessage("The estimated harvest date must be after the start date.");

        RuleFor(x => x.InitialStatus)
            .IsInEnum().WithMessage("A valid cycle status must be provided.");

        RuleFor(x => x.SeedQuantity)
            .GreaterThan(0).WithMessage("Seed quantity must be greater than zero.");

        RuleFor(x => x.SeedingArea)
            .GreaterThan(0).WithMessage("Seeding area must be greater than zero.");

        RuleFor(x => x.ExpectedYield)
            .GreaterThan(0).WithMessage("Expected yield must be greater than zero.");

        RuleFor(x => x.EnvironmentType)
            .IsInEnum()
            .WithMessage("Invalid environment type.");

        RuleFor(x => x.CropName)
            .NotEmpty().WithMessage("The crop name is required.")
            .MaximumLength(100).WithMessage("The crop name cannot exceed 100 characters.");
    }
}