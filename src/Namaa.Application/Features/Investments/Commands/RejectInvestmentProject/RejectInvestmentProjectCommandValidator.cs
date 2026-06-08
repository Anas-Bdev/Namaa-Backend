using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.RejectInvestmentProject;
public class RejectInvestmentProjectCommandValidator : AbstractValidator<RejectInvestmentProjectCommand>
{
    public RejectInvestmentProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("Project ID is required.");
    }
}