using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.SetActualResults;

public class SetActualResultsCommandValidator
    : AbstractValidator<SetActualResultsCommand>
{
    public SetActualResultsCommandValidator()
    {
        RuleFor(x => x.ActualRevenue)
            .GreaterThanOrEqualTo(0).WithMessage("Actual revenue cannot be negative.");
        RuleFor(x => x.ActualCost)
            .GreaterThanOrEqualTo(0).WithMessage("Actual cost cannot be negative.");
    }
}