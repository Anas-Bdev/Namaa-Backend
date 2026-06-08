using FluentValidation;

namespace Namaa.Application.Features.Experts.Commands.UpdateProfile;
public  class UpdateExpertAvailabilityCommandValidator : AbstractValidator<UpdateExpertAvailabilityCommand>
{
    public UpdateExpertAvailabilityCommandValidator()
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
