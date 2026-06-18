using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Commands.UpdateListing;
public sealed class UpdateProductListingCommandValidator : AbstractValidator<UpdateProductListingCommand>
{
    public UpdateProductListingCommandValidator(){

        RuleFor(x => x.ListingId)
            .NotEmpty().WithMessage("Listing ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Listing title cannot be empty.")
            .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

        RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("Unit of measurement (e.g., kg, ton) is required.")
            .MaximumLength(20).WithMessage("Unit name is too long.");

        RuleFor(x => x.PricePerUnit)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.QuantityAvailable)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");

        RuleFor(x => x.DiscountPrice)
            .LessThan(x => x.PricePerUnit)
            .When(x => x.DiscountPrice.HasValue)
            .WithMessage("Discount price must be lower than the original price.");

        RuleFor(x => x.CropName)
            .NotEmpty().WithMessage("Crop name is required.")
            .MaximumLength(100).WithMessage("Crop name cannot exceed 100 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(100).WithMessage("Category cannot exceed 100 characters.");

    }

}