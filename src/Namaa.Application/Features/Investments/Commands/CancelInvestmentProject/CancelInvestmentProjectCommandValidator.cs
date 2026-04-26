using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.CancelInvestmentProject;
public class CancelInvestmentProjectCommandValidator : AbstractValidator<CancelInvestmentProjectCommand>
{
     public CancelInvestmentProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("Project ID is required.");
    }
}