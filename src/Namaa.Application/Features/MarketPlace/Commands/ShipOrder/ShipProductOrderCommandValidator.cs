using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Commands.ShipOrder;
public sealed class ShipProductOrderCommandValidator : AbstractValidator<ShipProductOrderCommand>
{
    public ShipProductOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order ID is required to process payment.");
        
        RuleFor(x => x.EstimatedArrivalDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Estimated arrival date cannot be in the past.");
    }
}