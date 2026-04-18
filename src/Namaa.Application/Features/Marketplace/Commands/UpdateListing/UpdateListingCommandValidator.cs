using FluentValidation;

namespace Namaa.Application.Features.Marketplace.Commands.UpdateListing;

public class UpdateListingCommandValidator : AbstractValidator<UpdateListingCommand>
{
    public UpdateListingCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title is too long.");
        RuleFor(x => x.PricePerUnit)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(x => x.QuantityAvailable)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(x => x.DiscountPrice)
            .LessThan(x => x.PricePerUnit)
            .When(x => x.DiscountPrice.HasValue)
            .WithMessage("Discount price must be less than original price.");
    }
}