using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Commands.CreateOrder;
public sealed class CreateProductOrderCommandValidator : AbstractValidator<CreateProductOrderCommand>
{
    public CreateProductOrderCommandValidator()
    {
        RuleFor(x => x.ProductListingId)
            .NotEmpty().WithMessage("Product Listing ID is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.Governorate)
            .NotEmpty().WithMessage("Governorate is required.")
            .MaximumLength(100).WithMessage("Governorate cannot exceed 100 characters.");

        RuleFor(x => x.CityOrVillage)
            .NotEmpty().WithMessage("City or Village is required.")
            .MaximumLength(100).WithMessage("City or Village cannot exceed 100 characters.");

        RuleFor(x => x.NeighborhoodOrStreet)
            .NotEmpty().WithMessage("Neighborhood or Street is required.")
            .MaximumLength(250).WithMessage("Neighborhood or Street cannot exceed 250 characters.");
        
        RuleFor(x => x.LandMark)
            .MaximumLength(250).WithMessage("Landmark cannot exceed 250 characters.");

        RuleFor(x => x.DeliveryNotes)
            .MaximumLength(500).WithMessage("Delivery notes cannot exceed 500 characters.");
        
    }
}