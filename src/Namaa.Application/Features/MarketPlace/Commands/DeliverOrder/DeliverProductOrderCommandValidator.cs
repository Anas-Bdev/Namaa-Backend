using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Commands.DeliverOrder;
public sealed class DeliverProductOrderCommandValidator : AbstractValidator<DeliverProductOrderCommand>
{
    public DeliverProductOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order ID is required to confirm the inventory.");
    }
}