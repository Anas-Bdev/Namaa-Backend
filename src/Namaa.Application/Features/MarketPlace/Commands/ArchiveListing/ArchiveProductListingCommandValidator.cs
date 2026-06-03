using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Commands.ArchiveListing;
public sealed class ArchiveProductListingCommandValidator : AbstractValidator<ArchiveProductListingCommand>
{
    public ArchiveProductListingCommandValidator()
    {
        RuleFor(x => x.ListingId)
            .NotEmpty().WithMessage("Listing ID is required.");
    }
}