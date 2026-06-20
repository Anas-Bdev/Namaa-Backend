using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.AssignExpertToConsultation;

public class AssignExpertToConsultationCommandHandler(IAppDbContext context, INotificationService notificationService) : IRequestHandler<AssignExpertToConsultationCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(AssignExpertToConsultationCommand request, CancellationToken cancellationToken)
    {
        var consultation=await context.ConsultationRequests.FindAsync([request.ConsultationId],cancellationToken);

        if (consultation is null)
        {
            return ApplicationErrors.ConsultationNotFound;
        }

        var result = consultation.AssignExpert(request.ExpertId);

        if (result.IsError)
        {
            return result.Errors;
        }

        await context.SaveChangesAsync(cancellationToken);

        await notificationService.SendNotificationAsync(
        userId: request.ExpertId,
        title: "New Consultation Assigned",
        message: $"You have been assigned to a new consultation regarding '{consultation.Title}'.",
        type: "ConsultationAssigned",
        referencedId: consultation.Id
        );

        await notificationService.SendNotificationAsync(
        userId: consultation.FarmerId,
        title: "Expert Assigned",
        message: $"An agricultural expert has been assigned to your consultation regarding '{consultation.Title}' and will review it shortly.",
        type: "ConsultationUpdate",
        referencedId: consultation.Id
        );

        return Result.Updated;
    }
}