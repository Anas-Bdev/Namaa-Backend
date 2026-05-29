using FluentValidation;

namespace Namaa.Application.Features.Farmers.Commands.CreateProfile;

public class CreateFarmerProfileCommandValidator
    : AbstractValidator<CreateFarmerProfileCommand>
{
    public CreateFarmerProfileCommandValidator()
    {
       RuleFor(x => x.GovernorateId)
            .NotEmpty()
            .WithMessage("You must select a governorate.")
            .GreaterThan(0)
            .WithMessage("The selected governorate is invalid.");

        // 3. Description - Optional, but constrained if provided
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description is too long (max 500 characters).");

        // 4. AddressDetail - Optional, but constrained if provided
        RuleFor(x => x.AddressDetail)
            .MaximumLength(200)
            .WithMessage("Address detail is too long (max 200 characters).");
    }
}