using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Application.Features.Consultations.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Consultations;

namespace Namaa.Application.Features.Consultations.Commands.RequestConsultation;

public class RequestConsultationCommandHandler(IAppDbContext context,INotificationService notificationService) : IRequestHandler<RequestConsultationCommand, Result<RequestConsultationDto>>{
    public async Task<Result<RequestConsultationDto>> Handle(RequestConsultationCommand request, CancellationToken cancellationToken)
    {
        var consultationId = Guid.NewGuid();

        var consultationResult = ConsultationRequest.Create(
            consultationId,
            request.FarmerId,
            request.Title,
            request.Description,
            request.ImageUrl
        );

        if (consultationResult.IsError)
        {
            return consultationResult.Errors;
        }

        var consultation = consultationResult.Value;

        context.ConsultationRequests.Add(consultation);
        await context.SaveChangesAsync(cancellationToken);

        var experts = await context.ExpertProfiles.ToListAsync(cancellationToken);

        foreach (var expert in experts)
        {
            await notificationService.SendNotificationAsync(
                userId: expert.Id,
                title: "New Consultation Request 🌾",
                message: $"A farmer has submitted a new consultation: '{consultation.Title}'. Click to view and respond.",
                type: "NewConsultation",
                referencedId: consultation.Id
            );

    }
    
  return consultation.ToDto();

    }
}
