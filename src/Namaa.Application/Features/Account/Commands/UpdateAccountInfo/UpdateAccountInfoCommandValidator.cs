using FluentValidation;

namespace Namaa.Application.Features.Account.Commands.UpdateAccountInfo;
public class UpdateAccountInfoCommandValidator : AbstractValidator<UpdateAccountInfoCommand>
{
    public UpdateAccountInfoCommandValidator()
    {
         RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.LastName));

         RuleFor(x => x.PhoneNumber)
    .Matches(@"^(0|\+?970|\+?972)5[69]\d{7}$")
    .WithMessage("Please enter a valid Palestinian mobile number (e.g., 059... or 056...).")
    .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}