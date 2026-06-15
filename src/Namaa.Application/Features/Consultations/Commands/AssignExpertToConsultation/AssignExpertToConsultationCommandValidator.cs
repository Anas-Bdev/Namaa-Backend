using FluentValidation;

namespace Namaa.Application.Features.Consultations.Commands.AssignExpertToConsultation;
public sealed class AssignExpertToConsultationCommandValidator : AbstractValidator<AssignExpertToConsultationCommand>
{
    public AssignExpertToConsultationCommandValidator(){
        RuleFor(x => x.ConsultationId).NotEmpty();
        RuleFor(x => x.ExpertId).NotEmpty();
    }
}