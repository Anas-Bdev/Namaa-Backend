using FluentValidation;

namespace Namaa.Application.Features.Traders.Queries.GetTraderProfileById;
public class GetTraderProfileByIdQueryValidator : AbstractValidator<GetTraderProfileByIdQuery>
{
    public GetTraderProfileByIdQueryValidator()
    {
         RuleFor(x => x.TraderId)
            .NotEmpty()
            .WithMessage("Trader Id is required.");
    }
}