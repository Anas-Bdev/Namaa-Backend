using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Commands.CreateRating;
public sealed class CreateFarmerRatingCommandValidator : AbstractValidator<CreateFarmerRatingCommand>
{
    public CreateFarmerRatingCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Order ID is required to submit a rating.");
        
        RuleFor(x => x.RatingValue)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be strictly between 1 and 5 stars.");

        RuleFor(x => x.Comment)
            .MaximumLength(500)
            .WithMessage("Review comment cannot exceed 500 characters.");
    }
}