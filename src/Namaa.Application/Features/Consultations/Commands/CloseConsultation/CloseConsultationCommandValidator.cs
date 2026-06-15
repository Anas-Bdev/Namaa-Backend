using FluentValidation;
using Namaa.Application.Features.Consultations.Commands.CloseConsultation;

public sealed class CloseConsultationCommandValidator : AbstractValidator<CloseConsultationCommand>
{
    public CloseConsultationCommandValidator()
    {
        RuleFor(x => x.ConsultationId).NotEmpty();
    }
}