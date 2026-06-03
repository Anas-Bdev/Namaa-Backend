using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Queries.GetOrderById;
public sealed class GetOrderByIdQueryValidator : AbstractValidator<GetProductOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Order ID is required to retrieve details.");
    }
}