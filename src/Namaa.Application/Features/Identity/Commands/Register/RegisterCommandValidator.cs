using FluentValidation;

namespace Namaa.Application.Features.Identity.Commands.Register;
public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        // 1. Email Rules
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        // 2. Password Rules (Perfectly matched to your Identity Config)
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.") // RequiredLength = 8
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.") // RequireUppercase = true
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.") // RequireLowercase = true
            .Matches("[0-9]").WithMessage("Password must contain at least one number."); // RequireDigit = true


        // 3. Name Rules
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.LastName));

        // 4. Role Rules
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.");

        // 5. Phone Number Rules
           RuleFor(x => x.PhoneNumber)
           .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid international phone number format.")
           .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
            // Inside your RegisterCommandValidator constructor:
     
    }
}