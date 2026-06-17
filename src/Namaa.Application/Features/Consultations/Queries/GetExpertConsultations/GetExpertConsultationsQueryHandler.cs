using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Application.Features.Consultations.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetExpertConsultations;

public class GetExpertConsultationsQueryHandler(IAppDbContext context) : IRequestHandler<GetExpertConsultationsQuery, Result<List<RequestConsultationDto>>>{
    public async Task<Result<List<RequestConsultationDto>>> Handle(GetExpertConsultationsQuery request, CancellationToken cancellationToken)
    {
      var consultations = await context.ConsultationRequests
            .AsNoTracking()
            .Where(x => x.ExpertId == request.ExpertId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

     return consultations.ToDtos();
    }
}