using FluentValidation;

namespace Namaa.Application.Features.Identity.Commands.ForgotPassword;
public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email is required.")
        .EmailAddress().WithMessage("Please provide a valid email address");
    }
}