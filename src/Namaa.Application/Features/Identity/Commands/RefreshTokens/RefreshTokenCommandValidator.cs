using FluentValidation;

namespace Namaa.Application.Features.Identity.Commands.RefreshTokens;

public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");

        RuleFor(x => x.ExpiredAccessToken)
            .NotEmpty().WithMessage("The expired access token is required for rotation.");
    }
}