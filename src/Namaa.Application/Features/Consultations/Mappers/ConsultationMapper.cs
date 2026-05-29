using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Consultation;

namespace Namaa.Application.Features.Consultations.Mappers;

public static class ConsultationMapper
{
    public static ConsultationRequestDto ToDto(
        this ConsultationRequest request,
        string farmerName,
        Dictionary<Guid, string> expertNames)
    {
        return new ConsultationRequestDto
        {
            Id = request.Id,
            FarmerId = request.FarmerId,
            FarmerName = farmerName,
            Title = request.Title,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            Location = request.Location,
            Status = request.Status.ToString(),
            Responses = request.Responses
                .Select(r => r.ToDto(expertNames.GetValueOrDefault(r.ExpertId, string.Empty)))
                .ToList()
        };
    }

    public static ConsultationSummaryDto ToSummaryDto(
        this ConsultationRequest request,
        string farmerName)
    {
        return new ConsultationSummaryDto
        {
            Id = request.Id,
            FarmerId = request.FarmerId,
            FarmerName = farmerName,
            Title = request.Title,
            Status = request.Status.ToString(),
            ResponseCount = request.Responses.Count
        };
    }

    public static ExpertResponseDto ToDto(
        this ExpertResponse response,
        string expertName)
    {
        return new ExpertResponseDto
        {
            Id = response.Id,
            ExpertId = response.ExpertId,
            ExpertName = expertName,
            Message = response.Message,
            CreatedAt = response.CreatedAtUtc.DateTime
        };
    }
}