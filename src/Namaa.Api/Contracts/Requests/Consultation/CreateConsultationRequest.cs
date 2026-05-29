namespace Namaa.Api.Contracts.Requests.Consultation;

public record CreateConsultationRequest(
    string Title,
    string Description,
    string? ImageUrl,
    string? Location
);