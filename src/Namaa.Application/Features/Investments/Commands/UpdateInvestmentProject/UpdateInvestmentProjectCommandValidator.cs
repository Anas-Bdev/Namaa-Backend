using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.UpdateInvestmentProject;
public class UpdateInvestmentProjectCommandValidator : AbstractValidator<UpdateInvestmentProjectCommand>
{
     public UpdateInvestmentProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("Project ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Project title is required.")
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Project description is required.")
            .MaximumLength(2000);

        RuleFor(x => x.CoverImageUrl)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.CoverImageUrl));

        RuleFor(x => x.RequiredAmount)
            .GreaterThan(0)
            .WithMessage("Required amount must be greater than zero.");

        RuleFor(x => x.MinimumInvestment)
            .GreaterThan(0)
            .WithMessage("Minimum investment must be greater than zero.");

        RuleFor(x => x.MinimumInvestment)
            .LessThanOrEqualTo(x => x.RequiredAmount)
            .WithMessage("Minimum investment cannot exceed required amount.");

        RuleFor(x => x.FundingDeadline)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Funding deadline must be a future date.");

        RuleFor(x => x.ExpectedRevenue)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Expected revenue cannot be negative.");

        RuleFor(x => x.ExpectedCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Expected cost cannot be negative.");

        RuleFor(x => x.ExpectedCost)
            .LessThanOrEqualTo(x => x.ExpectedRevenue)
            .WithMessage("Expected cost cannot exceed expected revenue.");

        RuleFor(x => x.InvestorProfitSharePercentage)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithMessage("Investor profit share percentage must be greater than 0 and less than or equal to 100.");

        RuleFor(x => x.DurationInMonths)
            .GreaterThan(0)
            .WithMessage("Duration in months must be greater than zero.");
    }
}