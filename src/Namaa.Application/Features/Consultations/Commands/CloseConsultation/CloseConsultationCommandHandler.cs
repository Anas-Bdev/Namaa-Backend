using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.CloseConsultation;

public class CloseConsultationCommandHandler(IAppDbContext context) : IRequestHandler<CloseConsultationCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(CloseConsultationCommand request, CancellationToken cancellationToken)
    {
        var consultation=await context.ConsultationRequests.FindAsync([request.ConsultationId],cancellationToken);

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

        return Result.Updated;
    }
}