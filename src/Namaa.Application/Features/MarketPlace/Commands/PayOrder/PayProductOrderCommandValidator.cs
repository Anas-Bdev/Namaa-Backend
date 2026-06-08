using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Commands.PayOrder;
public sealed class PayProductOrderCommandValidator : AbstractValidator<PayProductOrderCommand>
{
    public PayProductOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order ID is required to process payment.");
    }
}