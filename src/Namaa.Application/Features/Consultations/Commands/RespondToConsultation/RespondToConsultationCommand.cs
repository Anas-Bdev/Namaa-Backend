using MediatR;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.RespondToConsultation;

public sealed record RespondToConsultationCommand(
    Guid RequestId,
    Guid ExpertId,
    string Message
) : IRequest<Result<ConsultationRequestDto>>;