using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Application.Features.Consultations.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Consultations;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Consultations.Queries.GetAvailableConsultations;

public class GetAvailableConsultationsQueryHandler(IAppDbContext context) : IRequestHandler<GetAvailableConsultationsQuery, Result<List<RequestConsultationDto>>>
{
    public async Task<Result<List<RequestConsultationDto>>> Handle(GetAvailableConsultationsQuery request, CancellationToken cancellationToken)
    {
        var pendingConsultations = await context.ConsultationRequests
            .AsNoTracking()
            .Where(c => c.Status == ConsultationStatus.Pending)
            .ToListAsync(cancellationToken);

        return pendingConsultations.ToDtos();
    }
}