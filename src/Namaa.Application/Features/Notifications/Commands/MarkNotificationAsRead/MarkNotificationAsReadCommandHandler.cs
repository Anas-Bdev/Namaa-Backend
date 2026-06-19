using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Notifications.Commands.MarkNotificationAsRead;

public class MarkNotificationAsReadCommandHandler(IAppDbContext context) : IRequestHandler<MarkNotificationAsReadCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await context.Notifications
            .FirstOrDefaultAsync(n => n.Id == request.Id && n.UserId == request.UserId, cancellationToken);

        if (notification is null)
        {
            return ApplicationErrors.NotificationNotFound;
        }

        notification.MarkAsRead();

        await context.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}