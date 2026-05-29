using FluentValidation;

namespace Namaa.Application.Features.Account.Commands.UpdateProfileImage;
public sealed class UpdateProfileImageCommandValidator : AbstractValidator<UpdateProfileImageCommand>
{
    public UpdateProfileImageCommandValidator()
    {
     RuleFor(x => x.FormFile)
            .NotNull().WithMessage("Image file is required.")
            .Must(file => file.Length > 0)
            .WithMessage("Image file cannot be empty.");   
    }
}