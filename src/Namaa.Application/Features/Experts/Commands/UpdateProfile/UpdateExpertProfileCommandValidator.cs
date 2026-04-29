using FluentValidation;

namespace Namaa.Application.Features.Experts.Commands.UpdateProfile;
public class UpdateExpertProfileCommandValidator : AbstractValidator<UpdateExpertProfileCommand>
{
    public UpdateExpertProfileCommandValidator()
    {
        RuleFor(x => x.Specialization)
            .IsInEnum().WithMessage("Please select a valid specialization.");

        RuleFor(x => x.YearsOfExperience)
            .GreaterThanOrEqualTo(0).WithMessage("Experience cannot be negative.")
            .LessThanOrEqualTo(60).WithMessage("Please enter a realistic number of years.");

        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("Please select a valid city.");

        RuleFor(x => x.AddressDetail)
            .NotEmpty().WithMessage("Address details are required.")
            .MaximumLength(500).WithMessage("Address is too long.");

        RuleFor(x => x.CanVisitOnSite)
            .NotNull().WithMessage("Please indicate if you can perform on-site visits.");

        RuleFor(x => x.Availabilities)
            .NotEmpty().WithMessage("You must provide at least one availability slot.")
            .Must(x => x.Count <= 50).WithMessage("Too many availability slots provided.");

        RuleForEach(x => x.Availabilities)
            .SetValidator(new CreateExpertAvailabilityCommandValidator());

      
    }


}