using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Commands.ResumeListing;
public sealed class ResumeProductListingCommandValidator : AbstractValidator<ResumeProductListingCommand>
{
    public ResumeProductListingCommandValidator()
    {
        RuleFor(x => x.ListingId)
            .NotEmpty().WithMessage("Listing ID is required.");
    }
}