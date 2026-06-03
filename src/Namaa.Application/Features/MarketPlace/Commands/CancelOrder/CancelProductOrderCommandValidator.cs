using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Commands.CancelOrder;
public sealed class CancelProductOrderCommandValidator : AbstractValidator<CancelProductOrderCommand>
{
    public CancelProductOrderCommandValidator()
    {
         RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order ID is required to confirm the inventory.");
    }
}