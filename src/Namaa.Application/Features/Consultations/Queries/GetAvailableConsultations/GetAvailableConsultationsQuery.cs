using MediatR;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetAvailableConsultations;
public sealed record GetAvailableConsultationsQuery():IRequest<Result<List<RequestConsultationDto>>>;