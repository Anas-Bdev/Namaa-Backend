using FluentValidation;

namespace Namaa.Application.Features.Identity.Commands.ResendConfirmationEmail;
public sealed class ResendConfirmationEmailCommandValidator : AbstractValidator<ResendConfirmationEmailCommand>
{
    public ResendConfirmationEmailCommandValidator()
    {
         RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email is required.")
        .EmailAddress().WithMessage("Please provide a valid email address");
    }
}