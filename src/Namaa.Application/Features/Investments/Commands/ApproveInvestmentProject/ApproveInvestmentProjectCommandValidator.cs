using FluentValidation;

namespace Namaa.Application.Features.Investments.Commands.ApproveInvestmentProject;
public class ApproveInvestmentProjectCommandValidator : AbstractValidator<ApproveInvestmentProjectCommand>
{
     public ApproveInvestmentProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("Project ID is required.");
    }
}