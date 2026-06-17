using MediatR;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetExpertConsultations;
public sealed record GetExpertConsultationsQuery(Guid ExpertId):IRequest<Result<List<RequestConsultationDto>>>;