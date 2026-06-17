using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.ConfirmContributionPayment;
public sealed class ConfirmContributionPaymentCommandValidator : AbstractValidator<ConfirmContributionPaymentCommand>
{
    public ConfirmContributionPaymentCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("Investment Project ID is required.");

        RuleFor(x => x.ContributionId)
            .NotEmpty()
            .WithMessage("Investor Contribution ID is required.");
    }
}