using MediatR;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.CreateConsultation;

public sealed record CreateConsultationCommand(
    Guid FarmerId,
    string Title,
    string Description,
    string? ImageUrl,
    string? Location
) : IRequest<Result<ConsultationRequestDto>>;