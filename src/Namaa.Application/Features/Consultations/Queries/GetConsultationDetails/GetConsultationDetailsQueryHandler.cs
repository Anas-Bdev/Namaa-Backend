using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Application.Features.Consultations.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetConsultationDetails;

public class GetConsultationDetailsQueryHandler(IAppDbContext context) : IRequestHandler<GetConsultationDetailsQuery, Result<ConsultationDetailsDto>>
{
    public async Task<Result<ConsultationDetailsDto>> Handle(GetConsultationDetailsQuery request, CancellationToken cancellationToken)
    {
        var consultation = await context.ConsultationRequests
            .AsNoTracking() 
            .Include(c => c.Messages.OrderBy(m => m.CreatedAtUtc)) 
            .FirstOrDefaultAsync(c => c.Id == request.ConsultationId, cancellationToken);

       if (consultation is null)
        {
            return ApplicationErrors.ConsultationNotFound; 
        }

        return consultation.ToDetailsDto();
    }
}