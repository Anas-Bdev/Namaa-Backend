using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Domain.Consultations;
public sealed class ConsultationRequest : AuditableEntity
{
    public Guid FarmerId {get;private set;}
    public Guid? ExpertId {get;private set;}
    public string Title {get;private set;}
    public string Description {get;private set;}
    public string? ImageUrl {get;private set;}
    public ConsultationStatus Status {get;private set;}
    private readonly List<ConsultationMessage> _messages=new();
    public IEnumerable<ConsultationMessage> Messages => _messages.AsReadOnly();
     
     #pragma warning disable CS8618
    private ConsultationRequest() {}
    #pragma warning restore CS8618

    private ConsultationRequest(Guid id, Guid farmerId, string title, string description, string? imageUrl) : base(id)
    {
        FarmerId = farmerId;
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        Status = ConsultationStatus.Pending;
    }

    public static Result<ConsultationRequest> Create(Guid id, Guid farmerId, string title, string description, string? imageUrl)
    {
        if (id == Guid.Empty) return ConsultationRequestErrors.IdRequired;
        if (farmerId == Guid.Empty) return ConsultationRequestErrors.FarmerIdRequired;
        if (string.IsNullOrWhiteSpace(title)) return ConsultationRequestErrors.TitleRequired;
        if (string.IsNullOrWhiteSpace(description)) return ConsultationRequestErrors.DescriptionRequired;

        return new ConsultationRequest(id, farmerId, title, description, imageUrl);
    }
    

    public Result<Updated> AddMessage(Guid senderId, string content)
    {
        if(Status==ConsultationStatus.Closed)
        return ConsultationRequestErrors.AlreadyClosed;

        if(string.IsNullOrWhiteSpace(content))
        return ConsultationRequestErrors.MessageContentRequired;

        _messages.Add(ConsultationMessage.Create(Id,senderId,content));
        return Result.Updated;
    }
    
        
    

    public Result<Updated> AssignExpert(Guid expertId)
    {
        if (Status != ConsultationStatus.Pending)
            return ConsultationRequestErrors.AlreadyClaimed;

        ExpertId = expertId;
        Status = ConsultationStatus.InProgress;
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