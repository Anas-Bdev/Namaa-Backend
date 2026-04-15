using FluentValidation;

namespace Namaa.Application.Features.Investors.Commands.CreateProfile;

public class CreateInvestorProfileCommandValidator
    : AbstractValidator<CreateInvestorProfileCommand>
{
    public CreateInvestorProfileCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("A valid User ID is required.");
    }
}