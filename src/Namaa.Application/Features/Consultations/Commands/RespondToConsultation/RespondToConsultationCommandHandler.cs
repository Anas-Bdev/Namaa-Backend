using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Application.Features.Consultations.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Consultation;

namespace Namaa.Application.Features.Consultations.Commands.RespondToConsultation;

public class RespondToConsultationCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<RespondToConsultationCommand, Result<ConsultationRequestDto>>
{
    public async Task<Result<ConsultationRequestDto>> Handle(
        RespondToConsultationCommand request,
        CancellationToken cancellationToken)
    {
        var consultation = await context.ConsultationRequests
            .Include(c => c.Responses)
            .FirstOrDefaultAsync(c => c.Id == request.RequestId, cancellationToken);

        if (consultation is null)
            return ApplicationErrors.ConsultationNotFound;

        var response = ExpertResponse.Create(
            Guid.NewGuid(),
            request.RequestId,
            request.ExpertId,
            request.Message
        );

        if (response.IsError)
            return response.Errors;

        var addResult = consultation.AddResponse(response.Value);
        if (addResult.IsError)
            return addResult.Errors;

        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var farmerName = users.FirstOrDefault(u => u.Id == consultation.FarmerId)?.FullName ?? string.Empty;
        var expertNames = consultation.Responses
            .ToDictionary(r => r.ExpertId,
                r => users.FirstOrDefault(u => u.Id == r.ExpertId)?.FullName ?? string.Empty);

        return consultation.ToDto(farmerName, expertNames);
    }
}