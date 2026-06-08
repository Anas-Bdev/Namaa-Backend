using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Commands.PauseListing;
public sealed class PauseProductListingCommandValidator : AbstractValidator<PauseProductListingCommand>
{
    public PauseProductListingCommandValidator()
    {
        RuleFor(x => x.ListingId)
            .NotEmpty().WithMessage("Listing ID is required.");
    }
}