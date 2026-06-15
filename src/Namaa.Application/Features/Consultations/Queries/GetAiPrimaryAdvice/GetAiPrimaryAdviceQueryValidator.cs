using FluentValidation;

namespace Namaa.Application.Features.Consultations.Queries.GetAiPrimaryAdvice;
public class GetAiPrimaryAdviceQueryValidator : AbstractValidator<GetAiPrimaryAdviceQuery>
{
    public GetAiPrimaryAdviceQueryValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required for AI analysis.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required for AI analysis.");
    }
}