using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Consultations;
public static class ConsultationRequestErrors
{
    public static readonly Error IdRequired=Error.Validation("Consultation.IdRequired","The consultation ID is required.");
    public static readonly Error FarmerIdRequired=Error.Validation("Consultation.FarmerIdRequired","The Farmer ID is required to open a consultation.");
    public static readonly Error TitleRequired=Error.Validation("Consultation.TitleRequired","A title is required for the consultation.");
    public static readonly Error MessageContentRequired=Error.Validation("Consultation.MessageContentRequired","Message content cannot be empty.");
    public static readonly Error DescriptionRequired=Error.Validation("Consultation.DescriptionRequired","A description of the problem is required.");
    public static readonly Error AlreadyClosed=Error.Conflict("ConsultationRequest.AlreadyClosed","This consultation request is already closed.");
    public static readonly Error AlreadyClaimed=Error.Conflict("ConsultationRequest.AlreadyClaimed","This consultation has already been claimed by an expert and is currently in progress.");
}