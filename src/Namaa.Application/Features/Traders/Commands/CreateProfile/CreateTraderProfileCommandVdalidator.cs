using FluentValidation;

namespace Namaa.Application.Features.Traders.Commands.CreateProfile;
public class CreateTraderProfileCommandValidator : AbstractValidator<CreateTraderProfileCommand>
{
    public CreateTraderProfileCommandValidator()
    {
        RuleFor(x => x.BusinessName)
            .NotEmpty().WithMessage("You must provide a Business Name.")
            .MaximumLength(200).WithMessage("Business name cannot exceed 200 characters.");

        // 2. Mandatory Business Type (Ensures they send a valid Enum value)
        RuleFor(x => x.TraderType)
            .IsInEnum().WithMessage("Please select a valid Business Type (Retailer, Wholesaler, etc.).");

        // 3. Mandatory Location for Search Filtering
        RuleFor(x => x.GovernorateId)
            .GreaterThan(0).WithMessage("Choosing a Governorate is mandatory for logistics.");

        // 4. Optional Detailed Address (Only validates length if they actually provide one)
        RuleFor(x => x.AddressDetail)
            .MaximumLength(500).WithMessage("Address detail is too long.")
            .When(x => !string.IsNullOrWhiteSpace(x.AddressDetail)); 
    }
}