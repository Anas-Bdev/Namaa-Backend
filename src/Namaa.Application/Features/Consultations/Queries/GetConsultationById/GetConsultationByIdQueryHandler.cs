using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Application.Features.Consultations.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetConsultationById;

public class GetConsultationByIdQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetConsultationByIdQuery, Result<ConsultationRequestDto>>
{
    public async Task<Result<ConsultationRequestDto>> Handle(
        GetConsultationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var consultation = await context.ConsultationRequests
            .AsNoTracking()
            .Include(c => c.Responses)
            .FirstOrDefaultAsync(c => c.Id == request.RequestId, cancellationToken);

        if (consultation is null)
            return ApplicationErrors.ConsultationNotFound;

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var farmerName = users.FirstOrDefault(u => u.Id == consultation.FarmerId)?.FullName ?? string.Empty;
        var expertNames = consultation.Responses
            .ToDictionary(r => r.ExpertId,
                r => users.FirstOrDefault(u => u.Id == r.ExpertId)?.FullName ?? string.Empty);

        return consultation.ToDto(farmerName, expertNames);
    }
}