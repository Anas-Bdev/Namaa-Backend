using FluentValidation;

namespace Namaa.Application.Features.Identity.Commands.LogOut;
public sealed class LogOutCommandValidator : AbstractValidator<LogOutCommand>
{
    public LogOutCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
        .NotEmpty().WithMessage("Refresh token is required to log out.");
    }
}