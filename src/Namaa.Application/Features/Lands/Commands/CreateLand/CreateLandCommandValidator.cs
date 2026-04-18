using FluentValidation;

namespace Namaa.Application.Features.Lands.Commands.CreateLand;

public sealed class CreateLandCommandValidator : AbstractValidator<CreateLandCommand>
{
    public CreateLandCommandValidator()
    {
        RuleFor(x => x.FarmerId)
            .NotEmpty()
            .WithMessage("Farmer ID is required.");

<<<<<<< HEAD
       RuleFor(x => x.AddressDetail)
            .NotEmpty().WithMessage("Address details are required.")
            .MinimumLength(5).WithMessage("Please provide a clearer address description.")
            .MaximumLength(250).WithMessage("Address description cannot exceed 250 characters.");


=======
>>>>>>> dev-alaa
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Land name is required.")
            .MaximumLength(200)
            .WithMessage("Land name cannot exceed 200 characters.");

        RuleFor(x => x.AreaDonum)
            .GreaterThan(0)
            .WithMessage("Area must be greater than zero.");

        RuleFor(x => x.CityId)
            .GreaterThan(0)
            .WithMessage("A valid City must be selected.");

        RuleFor(x => x.SoilId)
            .GreaterThan(0)
            .WithMessage("A valid Soil type must be selected.");

        RuleFor(x => x.WaterSourceType)
            .IsInEnum()
            .WithMessage("Invalid water source type selected.");

        RuleFor(x => x.WaterAvailability)
            .IsInEnum()
            .WithMessage("Invalid water availability selected.");

        RuleFor(x => x.EnvironmentType)
            .IsInEnum()
            .WithMessage("Invalid environment type selected.");
    }
}