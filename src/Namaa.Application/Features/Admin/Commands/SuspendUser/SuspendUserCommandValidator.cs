using FluentValidation;

namespace Namaa.Application.Features.Admin.Commands.SuspendUser;
public sealed class SuspendUserCommandValidator : AbstractValidator<SuspendUserCommand>
{
    public SuspendUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage("A reason for suspension must be provided.")
            .MinimumLength(5)
            .WithMessage("The suspension reason must be at least 5 characters long to provide sufficient detail.")
            .MaximumLength(500)
            .WithMessage("The suspension reason cannot exceed 500 characters.");    
    }
}