using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Consultations.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Consultations.Queries.GetMyResponses;

public sealed record GetMyResponsesQuery(
    Guid ExpertId,
    int PageNumber,
    int PageSize
) : IRequest<Result<PaginatedList<ConsultationSummaryDto>>>;