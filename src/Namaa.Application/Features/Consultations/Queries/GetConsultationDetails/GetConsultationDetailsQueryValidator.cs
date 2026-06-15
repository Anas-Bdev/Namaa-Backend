using FluentValidation;

namespace Namaa.Application.Features.Consultations.Queries.GetConsultationDetails;
public sealed class GetConsultationDetailsQueryValidator : AbstractValidator<GetConsultationDetailsQuery>
{
    public GetConsultationDetailsQueryValidator()
    {
        RuleFor(x => x.ConsultationId)
            .NotEmpty().WithMessage("Consultation ID is required.");
    }
}