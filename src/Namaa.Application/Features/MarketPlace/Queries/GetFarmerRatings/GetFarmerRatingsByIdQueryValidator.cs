using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Queries.GetFarmerRatings;
public sealed class GetFarmerRatingsQueryValidator : AbstractValidator<GetFarmerRatingsByIdQuery>
{
    public GetFarmerRatingsQueryValidator()
    {
        RuleFor(x => x.FarmerId)
            .NotEmpty()
            .WithMessage("Farmer ID must be a valid ID.");

    }
}