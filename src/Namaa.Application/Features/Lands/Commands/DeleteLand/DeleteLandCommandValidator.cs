using FluentValidation;

namespace Namaa.Application.Features.Lands.Commands.DeleteLand;

public sealed class DeleteLandCommandValidator : AbstractValidator<DeleteLandCommand>
{
    public DeleteLandCommandValidator()
    {
        RuleFor(x => x.LandId)
            .NotEmpty()
            .WithMessage("Land ID is required for deletion.");
    }
}