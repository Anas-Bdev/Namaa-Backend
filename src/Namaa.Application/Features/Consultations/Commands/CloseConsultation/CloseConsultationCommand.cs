using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.CloseConsultation;

public sealed record CloseConsultationCommand(
    Guid RequestId,
    Guid FarmerId
) : IRequest<Result<Updated>>;