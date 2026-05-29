using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Consultation;

public sealed class ExpertResponse : AuditableEntity
{
    public Guid RequestId { get; private set; }
    public ConsultationRequest? Request { get; private set; }
    public Guid ExpertId { get; private set; }
    public string Message { get; private set; } = string.Empty;

    private ExpertResponse() { }

    private ExpertResponse(
        Guid id,
        Guid requestId,
        Guid expertId,
        string message) : base(id)
    {
        RequestId = requestId;
        ExpertId = expertId;
        Message = message;
    }

    public static Result<ExpertResponse> Create(
        Guid id,
        Guid requestId,
        Guid expertId,
        string message)
    {
        if (id == Guid.Empty)
            return ExpertResponseErrors.IdRequired;
        if (string.IsNullOrWhiteSpace(message))
            return ExpertResponseErrors.MessageRequired;

        return new ExpertResponse(id, requestId, expertId, message);
    }
}