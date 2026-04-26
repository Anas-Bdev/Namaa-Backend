
using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.RejectInvestorContribution;
public class RejectInvestorContributionCommandValidator : AbstractValidator<RejectInvestorContributionCommand>
{
    public RejectInvestorContributionCommandValidator()
    {
        RuleFor(x => x.ContributionId)
            .NotEmpty()
            .WithMessage("Contribution ID is required.");
    }
}