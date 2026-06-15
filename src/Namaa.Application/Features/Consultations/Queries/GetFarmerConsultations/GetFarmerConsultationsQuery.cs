using MediatR;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetFarmerConsultations;
public sealed record GetFarmerConsultationQuery(Guid FarmerId):IRequest<Result<List<RequestConsultationDto>>>;