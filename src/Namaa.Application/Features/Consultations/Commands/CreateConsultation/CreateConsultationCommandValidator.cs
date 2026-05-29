using FluentValidation;

namespace Namaa.Application.Features.Consultations.Commands.CreateConsultation;

public class CreateConsultationCommandValidator
    : AbstractValidator<CreateConsultationCommand>
{
    public CreateConsultationCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title is too long.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description is too long.");
    }
}