using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Queries.GetFarmerRatings;
public sealed class GetFarmerRatingsByIdQueryValidator : AbstractValidator<GetFarmerRatingsByIdQuery>
{
    public GetFarmerRatingsByIdQueryValidator()
    {
        RuleFor(x => x.FarmerId)
            .NotEmpty()
            .WithMessage("Farmer ID must be a valid ID.");

    }
}