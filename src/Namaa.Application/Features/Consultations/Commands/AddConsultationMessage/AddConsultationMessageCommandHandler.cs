using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Consultations;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Consultations.Commands.AddConsultationMessage;

public class AddConsultationMessageCommandHandler(IAppDbContext context, INotificationService notificationService) : IRequestHandler<AddConsultationMessageCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(AddConsultationMessageCommand request, CancellationToken cancellationToken)
    {
        var consultation = await context.ConsultationRequests
            .FirstOrDefaultAsync(c => c.Id == request.ConsultationId, cancellationToken);

        if (consultation is null)
        {
            return ApplicationErrors.ConsultationNotFound;
        }

        if (consultation.Status == ConsultationStatus.Closed)
        return ConsultationRequestErrors.AlreadyClosed;
        
        var message = ConsultationMessage.Create(request.ConsultationId, request.SenderId, request.Content);
        
        await context.ConsultationMessages.AddAsync(message,cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        var targetUserId = request.SenderId == consultation.FarmerId 
            ? consultation.ExpertId 
            : consultation.FarmerId;

            if (targetUserId.HasValue) 
        {
            await notificationService.SendNotificationAsync(
                userId: targetUserId.Value,
                title: "New Message Received 💬",
                message: $"You have a new message in your consultation regarding '{consultation.Title}'.",
                type: "NewMessage",
                referencedId: consultation.Id
            );
        }

        return Result.Success;
    }
}