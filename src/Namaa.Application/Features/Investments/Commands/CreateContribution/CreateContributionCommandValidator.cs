using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.CreateContribution;

public class CreateContributionCommandValidator
    : AbstractValidator<CreateContributionCommand>
{
    public CreateContributionCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("Project ID is required.");
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Contribution amount must be greater than zero.");
    }
}