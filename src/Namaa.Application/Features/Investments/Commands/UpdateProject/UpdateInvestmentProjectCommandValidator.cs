using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.UpdateProject;

public class UpdateInvestmentProjectCommandValidator
    : AbstractValidator<UpdateInvestmentProjectCommand>
{
    public UpdateInvestmentProjectCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title is too long.");
        RuleFor(x => x.RequiredAmount)
            .GreaterThan(0).WithMessage("Required amount must be greater than zero.");
        RuleFor(x => x.ExpectedProfit)
            .GreaterThan(0).When(x => x.ExpectedProfit.HasValue)
            .WithMessage("Expected profit must be greater than zero.");
        RuleFor(x => x.SharePercentage)
            .InclusiveBetween(0, 100).When(x => x.SharePercentage.HasValue)
            .WithMessage("Share percentage must be between 0 and 100.");
    }
}