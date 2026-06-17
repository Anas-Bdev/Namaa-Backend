using MediatR;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetConsultationDetails;
public sealed record GetConsultationDetailsQuery(Guid ConsultationId):IRequest<Result<ConsultationDetailsDto>>;