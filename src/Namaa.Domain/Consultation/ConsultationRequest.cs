using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Domain.Consultation;

public sealed class ConsultationRequest : AuditableEntity
{
    public Guid FarmerId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string? ImageUrl { get; private set; }
    public string? Location { get; private set; }
    public ConsultationStatus Status { get; private set; } = ConsultationStatus.Pending;

    private readonly List<ExpertResponse> _responses = [];
    public IReadOnlyCollection<ExpertResponse> Responses => _responses.AsReadOnly();

    private ConsultationRequest() { }

    private ConsultationRequest(
        Guid id,
        Guid farmerId,
        string title,
        string description,
        string? imageUrl,
        string? location) : base(id)
    {
        FarmerId = farmerId;
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        Location = location;
    }

    public static Result<ConsultationRequest> Create(
        Guid id,
        Guid farmerId,
        string title,
        string description,
        string? imageUrl,
        string? location)
    {
        if (id == Guid.Empty)
            return ConsultationRequestErrors.IdRequired;
        if (string.IsNullOrWhiteSpace(title))
            return ConsultationRequestErrors.TitleRequired;
        if (string.IsNullOrWhiteSpace(description))
            return ConsultationRequestErrors.DescriptionRequired;

        return new ConsultationRequest(id, farmerId, title, description, imageUrl, location);
    }

    public Result<Updated> AddResponse(ExpertResponse response)
    {
        if (Status == ConsultationStatus.Closed)
            return ConsultationRequestErrors.AlreadyClosed;

        _responses.Add(response);
        Status = ConsultationStatus.Answered;
        return Result.Updated;
    }

    public Result<Updated> Close()
    {
        if (Status == ConsultationStatus.Closed)
            return ConsultationRequestErrors.AlreadyClosed;

        Status = ConsultationStatus.Closed;
        return Result.Updated;
    }
}