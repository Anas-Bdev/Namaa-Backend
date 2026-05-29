using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetMyConsultations;

public sealed record GetMyConsultationsQuery(
    Guid FarmerId,
    int PageNumber,
    int PageSize
) : IRequest<Result<PaginatedList<ConsultationSummaryDto>>>;