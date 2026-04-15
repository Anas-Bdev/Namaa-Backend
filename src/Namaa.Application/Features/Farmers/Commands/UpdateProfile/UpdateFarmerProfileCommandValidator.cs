using FluentValidation;

namespace Namaa.Application.Features.Farmers.Commands.UpdateProfile;

public class UpdateFarmerProfileCommandValidator
    : AbstractValidator<UpdateFarmerProfileCommand>
{
    public UpdateFarmerProfileCommandValidator()
    {
        RuleFor(x => x.CityId)
            .GreaterThan(0)
            .WithMessage("Please select a valid city.");

        RuleFor(x => x.AddressDetail)
            .MaximumLength(500)
            .WithMessage("Address is too long.");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Description is too long.");

        RuleFor(x => x.ExperienceLevel)
            .MaximumLength(100)
            .WithMessage("Experience level is too long.");
    }
}