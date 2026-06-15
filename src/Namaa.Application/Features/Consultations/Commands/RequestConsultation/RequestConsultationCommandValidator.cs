using FluentValidation;

namespace Namaa.Application.Features.Consultations.Commands.RequestConsultation;
public sealed class RequestConsultationCommandValidator : AbstractValidator<RequestConsultationCommand>
{
    public RequestConsultationCommandValidator()
    {
        RuleFor(x => x.FarmerId)
            .NotEmpty().WithMessage("Farmer ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("A title is required.")
            .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A description of the agricultural problem is required.")
            .MinimumLength(10).WithMessage("Please provide at least 10 characters so the expert understands the issue.");
    }
}