namespace Namaa.Application.Features.Lands.Queries.GetLandById;
using FluentValidation;
public sealed class GetLandByIdQueryValidator : AbstractValidator<GetLandByIdQuery>
{
    public GetLandByIdQueryValidator()
    {
        RuleFor(x => x.LandId)
            .NotEmpty()
            .WithMessage("Land ID is required to fetch details.");
    }
}