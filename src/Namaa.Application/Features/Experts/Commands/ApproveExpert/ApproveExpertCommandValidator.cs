using FluentValidation;

namespace Namaa.Application.Features.Experts.Commands.ApproveExpert;
public class ApproveExpertCommandValidator : AbstractValidator<ApproveExpertCommand>
{
    public ApproveExpertCommandValidator()
    {
        RuleFor(x => x.ExpertId)
            .NotEmpty()
            .WithMessage("Expert ID is required.");
    }
}