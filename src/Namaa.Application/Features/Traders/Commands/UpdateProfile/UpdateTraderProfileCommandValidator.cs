using FluentValidation;

namespace Namaa.Application.Features.Traders.Commands.UpdateProfile;

public class UpdateTraderProfileCommandValidator
    : AbstractValidator<UpdateTraderProfileCommand>
{
    public UpdateTraderProfileCommandValidator()
    {
        RuleFor(x => x.BusinessName)
            .NotEmpty().WithMessage("Business name is required.")
            .MaximumLength(200).WithMessage("Business name is too long.");
        RuleFor(x => x.BusinessType)
            .NotEmpty().WithMessage("Business type is required.")
            .MaximumLength(100).WithMessage("Business type is too long.");
        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("Please select a valid city.");
        RuleFor(x => x.AddressDetail)
            .MaximumLength(500).WithMessage("Address is too long.");
        RuleFor(x => x.PreferredCategories)
            .MaximumLength(500).WithMessage("Preferred categories is too long.");
    }
}
