using FluentValidation;

namespace Namaa.Application.Features.Farmers.Commands.CreateProfile;

public class CreateFarmerProfileCommandValidator
    : AbstractValidator<CreateFarmerProfileCommand>
{
    public CreateFarmerProfileCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("A valid User ID is required.");
    }
}