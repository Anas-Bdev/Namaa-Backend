using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.RespondToContribution;

public class RespondToContributionCommandValidator
    : AbstractValidator<RespondToContributionCommand>
{
    public RespondToContributionCommandValidator()
    {
        RuleFor(x => x.ContributionId)
            .NotEmpty().WithMessage("Contribution ID is required.");
    }
}   
