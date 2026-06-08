using FluentValidation;

namespace Namaa.Application.Features.Farmers.Queries.GetFarmerProfileById;
public class GetFarmerProfileByIdQueryValidator 
    : AbstractValidator<GetFarmerProfileByIdQuery>
{
    public GetFarmerProfileByIdQueryValidator()
    {
        RuleFor(x => x.FarmerId)
            .NotEmpty()
            .WithMessage("Farmer Id is required.");
    }
}