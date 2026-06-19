using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.CloseConsultation;
public sealed record CloseConsultationCommand(
    Guid ConsultationId,
    Guid CloseByUserId
):IRequest<Result<Updated>>;