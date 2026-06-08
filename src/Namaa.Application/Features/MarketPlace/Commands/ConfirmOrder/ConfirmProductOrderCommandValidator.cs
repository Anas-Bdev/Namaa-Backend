using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Commands.ConfirmOrder;
public sealed class ConfirmProductOrderCommandValidator : AbstractValidator<ConfirmProductOrderCommand>
{
    public ConfirmProductOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order ID is required to confirm the inventory.");
    }
}