using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Namaa.Application.Features.Experts.Commands.RegisterExpert;
public sealed class RegisterExpertCommandValidator : AbstractValidator<RegisterExpertCommand>
{
    public RegisterExpertCommandValidator()
    {
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

       RuleFor(x => x.PhoneNumber)
    .Matches(@"^(0|\+?970|\+?972)5[69]\d{7}$")
    .WithMessage("Please enter a valid Palestinian mobile number (e.g., 059... or 056...).")
    .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

     RuleFor(x => x.CvFile)
            .NotNull().WithMessage("A CV file is required to create a profile.");

        RuleFor(x => x.CvFile)
            .Must(file => file == null || IsPdf(file))
            .WithMessage("Only PDF files are allowed for CV uploads.");

        RuleFor(x => x.CvFile)
            .Must(file => file == null || file.Length <= 5 * 1024 * 1024)
            .WithMessage("CV file size must not exceed 5MB.");


    }

     private bool IsPdf(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLower();
        return extension == ".pdf" || file.ContentType == "application/pdf";
    }
}