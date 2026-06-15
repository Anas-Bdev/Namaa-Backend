using System.Transactions;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Consultations;

namespace Namaa.Application.Features.Consultations.Mappers;
public static class ConsultationsMapper
{
    public static RequestConsultationDto ToDto(this ConsultationRequest entity)
    {
        return new RequestConsultationDto
        {
            Id=entity.Id,
            FarmerId=entity.FarmerId,
            Title=entity.Title,
            Description=entity.Description,
            Status=entity.Status,
            ImageUrl=entity.ImageUrl
        };
    }

    public static List<RequestConsultationDto> ToDtos(this IEnumerable<ConsultationRequest> entities)
    {
        return [.. entities.Select(e => e.ToDto())];
    }

    public static MessageDto ToDto(this ConsultationMessage entity)
    {
        return new MessageDto
        {
            Id = entity.Id,
            SenderId = entity.SenderId,
            Content = entity.Content,
            CreatedAt = entity.CreatedAtUtc
        };
    }

    public static ConsultationDetailsDto ToDetailsDto(this ConsultationRequest entity)
    {
        return new ConsultationDetailsDto
        {
            Id = entity.Id,
            FarmerId = entity.FarmerId,
            ExpertId = entity.ExpertId,
            Title = entity.Title,
            Description = entity.Description,
            Status = entity.Status.ToString(),
            ImageUrl = entity.ImageUrl,
            
            Messages = [.. entity.Messages.Select(m => m.ToDto())]
        };
    }
}