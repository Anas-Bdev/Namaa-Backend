using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.StartInvestmentProject;
public class StartInvestmentProjectCommandValidator : AbstractValidator<StartInvestmentProjectCommand>
{
    public StartInvestmentProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("Project ID is required.");
    }
}