using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Application.Features.Consultations.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Consultation;

namespace Namaa.Application.Features.Consultations.Commands.CreateConsultation;

public class CreateConsultationCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<CreateConsultationCommand, Result<ConsultationRequestDto>>
{
    public async Task<Result<ConsultationRequestDto>> Handle(
        CreateConsultationCommand request,
        CancellationToken cancellationToken)
    {
        var result = ConsultationRequest.Create(
            Guid.NewGuid(),
            request.FarmerId,
            request.Title,
            request.Description,
            request.ImageUrl,
            request.Location
        );

        if (result.IsError)
            return result.Errors;

        context.ConsultationRequests.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var farmerName = users.FirstOrDefault(u => u.Id == request.FarmerId)?.FullName ?? string.Empty;

        return result.Value.ToDto(farmerName, new Dictionary<Guid, string>());
    }
}