using FluentValidation;

namespace Namaa.Application.Features.Consultations.Commands.RespondToConsultation;

public class RespondToConsultationCommandValidator
    : AbstractValidator<RespondToConsultationCommand>
{
    public RespondToConsultationCommandValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Response message is required.")
            .MaximumLength(2000).WithMessage("Message is too long.");
    }
}