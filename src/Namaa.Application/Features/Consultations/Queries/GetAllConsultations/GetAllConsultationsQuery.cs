using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Consultations.Queries.GetAllConsultations;

public sealed record GetAllConsultationsQuery(
    int PageNumber,
    int PageSize,
    ConsultationStatus? Status
) : IRequest<Result<PaginatedList<ConsultationSummaryDto>>>;