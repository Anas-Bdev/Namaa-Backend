using FluentValidation;
using FluentValidation.Validators;

namespace Namaa.Application.Features.Experts.Commands.UpdateProfile;
public  class CreateExpertAvailabilityCommandValidator : AbstractValidator<UpdateExpertAvailabilityCommand>
{
    public CreateExpertAvailabilityCommandValidator()
    {
        RuleFor(x => x.Day)
            .IsInEnum().WithMessage("Invalid day of the week.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required.")
            .Must((model, endTime) => endTime > model.StartTime)
            .WithMessage("End time must be after the start time.");
    }
}
