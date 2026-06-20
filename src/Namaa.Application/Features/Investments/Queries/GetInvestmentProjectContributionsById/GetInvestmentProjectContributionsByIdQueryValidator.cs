using FluentValidation;
using Namaa.Application.Features.Investments.Queries.GetInvestmentProjectContributionsByIdj;

namespace Namaa.Application.Features.Investments.Queries.GetInvestmentProjectContributionsById;
public class GetInvestmentProjectContributionsByIdQueryValidator : AbstractValidator<GetInvestmentProjectContributionsByIdQuery>
{
    public GetInvestmentProjectContributionsByIdQueryValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("Project ID is required and cannot be empty.");
    }
}