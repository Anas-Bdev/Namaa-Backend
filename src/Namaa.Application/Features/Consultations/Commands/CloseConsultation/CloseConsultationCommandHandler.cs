using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.CloseConsultation;

public class CloseConsultationCommandHandler(IAppDbContext context, INotificationService notificationService) : IRequestHandler<CloseConsultationCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(CloseConsultationCommand request, CancellationToken cancellationToken)
    {
        var consultation = await context.ConsultationRequests
            .FirstOrDefaultAsync(c => c.Id == request.ConsultationId, cancellationToken);

        if (consultation is null)
        {
            return ApplicationErrors.ConsultationNotFound; 
        }

        var result = consultation.Close();

        if (result.IsError)
        {
            return result.Errors;
        }

        await context.SaveChangesAsync(cancellationToken);

        var targetUserId = request.CloseByUserId == consultation.FarmerId 
            ? consultation.ExpertId 
            : consultation.FarmerId;

        if (targetUserId.HasValue)
        {
            await notificationService.SendNotificationAsync(
                userId: targetUserId.Value,
                title: "Consultation Closed",
                message: $"The consultation '{consultation.Title}' has been closed. Thank you!",
                type: "ConsultationClosed",
                referencedId: consultation.Id
            );
        }

        return Result.Updated;
    }
}