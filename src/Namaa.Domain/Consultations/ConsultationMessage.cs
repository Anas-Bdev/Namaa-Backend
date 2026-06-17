using Namaa.Domain.Common;

namespace Namaa.Domain.Consultations;
public sealed class ConsultationMessage : AuditableEntity
{
    public Guid ConsultationRequestId { get; private set; }
    public Guid SenderId { get; private set; }
    public string Content { get; private set; }

    #pragma warning disable CS8618
     private ConsultationMessage() { }
    #pragma warning restore CS8618

    private ConsultationMessage(Guid id, Guid requestId, Guid senderId, string content) : base(id)
    {
            ConsultationRequestId = requestId;
            SenderId = senderId;
            Content = content;
    }

    public static ConsultationMessage Create(Guid requestId, Guid senderId, string content)
    {
           
            
        return new ConsultationMessage(Guid.NewGuid(), requestId, senderId, content);
    }
}
