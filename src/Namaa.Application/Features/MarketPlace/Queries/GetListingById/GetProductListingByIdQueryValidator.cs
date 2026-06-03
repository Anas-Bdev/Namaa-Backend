using FluentValidation;

namespace Namaa.Application.Features.MarketPlace.Queries.GetListingById;
public sealed class GetProductListingByIdQueryValidator : AbstractValidator<GetProductListingByIdQuery>
{
    public GetProductListingByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID must be provided.");
    }
}