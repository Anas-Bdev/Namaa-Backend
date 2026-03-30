using FluentValidation;

namespace Namaa.Application.Features.Traders.Commands.CreateProfile;

public class CreateTraderProfileCommandValidator
    : AbstractValidator<CreateTraderProfileCommand>
{
    public CreateTraderProfileCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("A valid User ID is required.");
    }
}
