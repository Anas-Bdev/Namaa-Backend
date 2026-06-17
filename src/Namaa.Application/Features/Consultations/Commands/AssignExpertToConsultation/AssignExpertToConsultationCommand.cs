using MediatR;
using Namaa.Domain.Common.Results;

public sealed record AssignExpertToConsultationCommand(
    Guid ConsultationId,
    Guid ExpertId
):IRequest<Result<Updated>>;