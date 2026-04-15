using FluentValidation;

namespace Namaa.Application.Features.Experts.Queries.GetExpertProfileById;
public class GetExpertProfileByIdQueryValidator : AbstractValidator<GetExpertProfileByIdQuery>
{
     public GetExpertProfileByIdQueryValidator()
    {
        RuleFor(x => x.ExpertId)
            .NotEmpty()
            .WithMessage("Expert Id is required.");
    }
}