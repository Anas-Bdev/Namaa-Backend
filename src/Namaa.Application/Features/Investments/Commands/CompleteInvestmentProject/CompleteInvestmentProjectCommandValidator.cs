using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.CompleteInvestmentProject;
public class CompleteInvestmentProjectCommandValidator : AbstractValidator<CompleteInvestmentProjectCommand>
{
    public CompleteInvestmentProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("Project ID is required.");

        RuleFor(x => x.ActualRevenue)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Actual revenue cannot be negative.");

        RuleFor(x => x.ActualCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Actual cost cannot be negative.");
    }
}