using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Application.Features.Consultations.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetFarmerConsultations;

public class GetFarmerConsultationsQueryHandler(IAppDbContext context) : IRequestHandler<GetFarmerConsultationQuery, Result<List<RequestConsultationDto>>>
{
    public async Task<Result<List<RequestConsultationDto>>> Handle(GetFarmerConsultationQuery request, CancellationToken cancellationToken)
    {
        var consultations=await context.ConsultationRequests
                                .AsNoTracking()
                                .Where(x => x.FarmerId==request.FarmerId)
                                .ToListAsync(cancellationToken);

        return consultations.ToDtos();
    }
}