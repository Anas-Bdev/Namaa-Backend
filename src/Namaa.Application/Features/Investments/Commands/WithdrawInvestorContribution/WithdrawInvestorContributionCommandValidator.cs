using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.WithdrawInvestorContribution;
public sealed class WithdrawInvestorContributionCommandValidator : AbstractValidator<WithdrawInvestorContributionCommand>
{
    public WithdrawInvestorContributionCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("Investment Project ID is required.");

        RuleFor(x => x.ContributionId)
            .NotEmpty()
            .WithMessage("Investor Contribution ID is required.");

        RuleFor(x => x.InvestorId)
            .NotEmpty()
            .WithMessage("Investor ID is required.");
    }
}