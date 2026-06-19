namespace Namaa.Application.Common.Interfaces;
public interface INotificationService
{
    Task SendNotificationAsync(Guid userId, string title, string message, string type, Guid? referencedId = null, Guid? triggeredBy = null,CancellationToken cancellationToken=default);
}