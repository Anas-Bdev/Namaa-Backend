using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Commands.AddConsultationMessage;
public sealed record AddConsultationMessageCommand(
    Guid ConsultationId,
    Guid SenderId,
    string Content
):IRequest<Result<Success>>;