using FluentValidation;

namespace Namaa.Application.Features.Lookups.Queries.GetCrops;
public class GetCropsQueryValidator : AbstractValidator<GetCropsQuery>
{
    public GetCropsQueryValidator()
    {
         RuleFor(x => x.LandId)
            .NotEmpty()
            .WithMessage("Land Id is required.");
    }
}