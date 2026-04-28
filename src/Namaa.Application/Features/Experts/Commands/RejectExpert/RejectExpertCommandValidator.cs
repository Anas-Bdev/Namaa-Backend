using FluentValidation;

namespace Namaa.Application.Features.Experts.Commands.RejectExpert;
public class RejectExpertCommandValidator : AbstractValidator<RejectExpertCommand>
{
    public RejectExpertCommandValidator()
    {
        RuleFor(x => x.ExpertId)
            .NotEmpty()
            .WithMessage("Expert id is required.");

        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage("Rejection reason is required.")
            .MaximumLength(500)
            .WithMessage("Rejection reason must not exceed 500 characters.");
    }
}