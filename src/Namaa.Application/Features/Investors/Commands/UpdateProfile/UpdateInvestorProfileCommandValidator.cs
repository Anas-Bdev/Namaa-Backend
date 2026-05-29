using FluentValidation;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investors.Commands.UpdateProfile;
public class UpdateInvestorProfileCommandValidator : AbstractValidator<UpdateInvestorProfileCommand>
{
    public UpdateInvestorProfileCommandValidator()
    {
         RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Please select a valid Investor Type.");

        // 3. Mandatory Location
        RuleFor(x => x.GovernorateId)
            .GreaterThan(0).WithMessage("Choosing a Governorate is mandatory.");

        // 4. Conditional Business Logic (The DDD rule)
        RuleFor(x => x.OrganizationName)
            .NotEmpty().WithMessage("Organization name is required for Corporate or NGO investors.")
            .MaximumLength(200).WithMessage("Organization name cannot exceed 200 characters.")
            .When(x => x.Type != InvestorType.Individual); 

        // 5. Optional Detailed Address
        RuleFor(x => x.AddressDetail)
            .MaximumLength(500).WithMessage("Address detail is too long.")
            .When(x => !string.IsNullOrWhiteSpace(x.AddressDetail));
    }
}