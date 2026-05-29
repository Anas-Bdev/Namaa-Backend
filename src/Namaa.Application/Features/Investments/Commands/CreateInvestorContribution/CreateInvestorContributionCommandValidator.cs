using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.CreateInvestorContribution;
public class CreateInvestorContributionCommandValidator : AbstractValidator<CreateInvestorContributionCommand>
{
     public CreateInvestorContributionCommandValidator()
    {
        RuleFor(x => x.InvestmentProjectId)
            .NotEmpty()
            .WithMessage("Investment project ID is required.");

        
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Contribution amount must be greater than zero.");
    }
}