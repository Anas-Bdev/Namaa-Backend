using System.IO.Pipes;
using System.Net.NetworkInformation;
using Namaa.Domain.Common;

namespace Namaa.Domain.Notifications;
public sealed class Notification : AuditableEntity
{
    public Guid UserId {get;private set;}
    public string Title {get;private set;}
    public string Message {get; private set;}
    public bool IsRead {get;private set;}
    public string Type {get;private set;}
    public Guid? ReferencedId {get;private set;}
    public Guid? TriggeredBy {get;private set;}
     
    #pragma warning disable CS8618
    private Notification() {}
    #pragma warning restore CS8618

    private Notification(Guid id, Guid userId, string title, string message, string type, Guid? referencedId, Guid? triggeredBy) 
        : base(id) 
    {
        UserId = userId;
        Title = title;
        Message = message;
        Type = type;
        ReferencedId = referencedId;
        TriggeredBy = triggeredBy;
    }

    public static Notification Create(Guid userId, string title, string message, string type, Guid? referencedId = null, Guid? triggeredBy = null)
    {
        if (userId == Guid.Empty) throw new ArgumentException("UserId is required.");
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required.");
        if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Message is required.");

        return new Notification(Guid.NewGuid(), userId, title, message, type, referencedId, triggeredBy);
    }

    public void MarkAsRead()
    {
        if (!IsRead)
        {
            IsRead = true;
        }
    }
}