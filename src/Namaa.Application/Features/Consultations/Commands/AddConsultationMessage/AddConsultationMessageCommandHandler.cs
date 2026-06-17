using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.AddConsultationMessage;

public class AddConsultationMessageCommandHandler(IAppDbContext context) : IRequestHandler<AddConsultationMessageCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(AddConsultationMessageCommand request, CancellationToken cancellationToken)
    {
        var consultation=await context.ConsultationRequests.FindAsync([request.ConsultationId],cancellationToken);

        if (consultation is null)
        {
            return ApplicationErrors.ConsultationNotFound;
        }

        var result = consultation.AddMessage(request.SenderId, request.Content);

        if (result.IsError)
        {
            return result.Errors;
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}