using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Notifications;

namespace Namaa.Infrastructure.Services;

public class NotificationService(IAppDbContext context) : INotificationService
{
    public async Task SendNotificationAsync(Guid userId, string title, string message, string type, Guid? referencedId = null, Guid? triggeredBy = null,CancellationToken cancellationToken=default)
    {
        var notification= Notification.Create(userId,title,message,type,referencedId,triggeredBy);
        
        await context.Notifications.AddAsync(notification);

        await context.SaveChangesAsync(cancellationToken);
    


    }
}