using MediatR;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetConsultationById;

public sealed record GetConsultationByIdQuery(Guid RequestId)
    : IRequest<Result<ConsultationRequestDto>>;