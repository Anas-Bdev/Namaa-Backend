using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Consultation;

public static class ConsultationRequestErrors
{
    public static readonly Error IdRequired = Error.Validation(
        "ConsultationRequest.IdRequired", "A valid ID must be provided.");
    public static readonly Error TitleRequired = Error.Validation(
        "ConsultationRequest.TitleRequired", "Title is required.");
    public static readonly Error DescriptionRequired = Error.Validation(
        "ConsultationRequest.DescriptionRequired", "Description is required.");
    public static readonly Error AlreadyClosed = Error.Conflict(
        "ConsultationRequest.AlreadyClosed", "This consultation request is already closed.");
    public static readonly Error InvalidStatusTransition = Error.Conflict(
        "ConsultationRequest.InvalidStatus", "Invalid status transition.");
}