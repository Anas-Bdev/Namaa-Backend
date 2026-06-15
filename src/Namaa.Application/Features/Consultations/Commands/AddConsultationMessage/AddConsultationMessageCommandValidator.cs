using FluentValidation;

namespace Namaa.Application.Features.Consultations.Commands.AddConsultationMessage;
public sealed class AddConsultationMessageCommandValidator : AbstractValidator<AddConsultationMessageCommand>
{
    public AddConsultationMessageCommandValidator()
    {
        RuleFor(x => x.ConsultationId)
            .NotEmpty().WithMessage("Consultation ID is required.");

        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("Sender ID is required.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Message content cannot be empty.")
            .MaximumLength(1000).WithMessage("Message is too long. Please keep it under 1000 characters.");
    }
}