using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.ApproveInvestorContribution;
public class ApproveInvestorContributionCommandValidator : AbstractValidator<ApproveInvestorContributionCommand>
{
     public ApproveInvestorContributionCommandValidator()
    {
        RuleFor(x => x.ContributionId)
            .NotEmpty()
            .WithMessage("Contribution ID is required.");
    }
}