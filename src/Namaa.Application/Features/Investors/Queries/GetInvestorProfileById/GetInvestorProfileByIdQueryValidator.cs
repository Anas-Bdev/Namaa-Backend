using FluentValidation;

namespace Namaa.Application.Features.Investors.Queries.GetInvestorProfileById;
public class GetInvestorProfileByIdQueryValidator : AbstractValidator<GetInvestorProfileByIdQuery>
{
    public GetInvestorProfileByIdQueryValidator()
    {
        
        RuleFor(x => x.InvestorId)
            .NotEmpty()
            .WithMessage("Investor Id is required.");
    }
}