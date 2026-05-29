using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Consultation;

namespace Namaa.Application.Features.Consultations.Commands.CloseConsultation;

public class CloseConsultationCommandHandler(IAppDbContext context)
    : IRequestHandler<CloseConsultationCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(
        CloseConsultationCommand request,
        CancellationToken cancellationToken)
    {
        var consultation = await context.ConsultationRequests
            .FirstOrDefaultAsync(c => c.Id == request.RequestId, cancellationToken);

        if (consultation is null)
            return ApplicationErrors.ConsultationNotFound;

        if (consultation.FarmerId != request.FarmerId)
            return ApplicationErrors.Forbidden;

        var result = consultation.Close();
        if (result.IsError)
            return result.Errors;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}