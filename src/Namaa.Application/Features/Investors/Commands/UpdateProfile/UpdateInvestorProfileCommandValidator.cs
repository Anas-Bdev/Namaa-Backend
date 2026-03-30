using FluentValidation;

namespace Namaa.Application.Features.Investors.Commands.UpdateProfile;

public class UpdateInvestorProfileCommandValidator
    : AbstractValidator<UpdateInvestorProfileCommand>
{
    public UpdateInvestorProfileCommandValidator()
    {
        RuleFor(x => x.OrganizationName)
            .NotEmpty().WithMessage("Organization name is required.")
            .MaximumLength(200).WithMessage("Organization name is too long.");
        RuleFor(x => x.CompanyName)
            .MaximumLength(200).WithMessage("Company name is too long.");
        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("Please select a valid city.");
        RuleFor(x => x.AddressDetail)
            .MaximumLength(500).WithMessage("Address is too long.");
    }
}