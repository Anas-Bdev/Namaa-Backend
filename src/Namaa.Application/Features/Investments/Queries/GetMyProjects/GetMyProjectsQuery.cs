using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetMyProjects;

public sealed record GetMyProjectsQuery(
    Guid UserId,
    int PageNumber,
    int PageSize
) : IRequest<Result<PaginatedList<InvestmentProjectSummaryDto>>>;