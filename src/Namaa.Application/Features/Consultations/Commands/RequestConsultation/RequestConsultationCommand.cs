using MediatR;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.RequestConsultation;
public sealed record RequestConsultationCommand(
    Guid FarmerId,
    string Title,
    string Description,
    string? ImageUrl
):IRequest<Result<RequestConsultationDto>>;