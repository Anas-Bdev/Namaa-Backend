using FluentValidation;
namespace Namaa.Application.Features.MarketPlace.Commands.CreateListing;
public sealed class CreateProductListingCommandValidator : AbstractValidator<CreateProductListingCommand>
{
    public CreateProductListingCommandValidator()
    {
        RuleFor(x => x.FarmerId)
            .NotEmpty().WithMessage("Farmer ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Listing title cannot be empty.")
            .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

        RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("Unit of measurement (e.g., kg, ton) is required.")
            .MaximumLength(20).WithMessage("Unit name is too long.");

        RuleFor(x => x.PricePerUnit)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.QuantityAvailable)
            .GreaterThan(0).WithMessage("Initial quantity must be greater than zero.");

        RuleFor(x => x.DiscountPrice)
            .LessThan(x => x.PricePerUnit)
            .When(x => x.DiscountPrice.HasValue)
            .WithMessage("Discount price must be lower than the original price.");

        RuleFor(x => x.HarvestDate)
            .NotEmpty()
            .When(x => !x.SeedingCycleId.HasValue)
            .WithMessage("Harvest Date is mandatory for immediate inventory sales (when no Seeding Cycle is linked).");

        RuleFor(x => x.CropName)
            .NotEmpty().WithMessage("Crop name is required.")
            .MaximumLength(100).WithMessage("Crop name cannot exceed 100 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(100).WithMessage("Category cannot exceed 100 characters.");
    }
}