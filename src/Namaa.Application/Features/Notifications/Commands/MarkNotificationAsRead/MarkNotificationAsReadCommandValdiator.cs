using FluentValidation;

namespace Namaa.Application.Features.Notifications.Commands.MarkNotificationAsRead;
public sealed class MarkNotificationAsReadCommandValidator : AbstractValidator<MarkNotificationAsReadCommand>
{
    public MarkNotificationAsReadCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Notification ID is required.");

       
    }
}