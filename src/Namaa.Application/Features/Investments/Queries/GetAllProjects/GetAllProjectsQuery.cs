using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Investments.Queries.GetAllProjects;

public sealed record GetAllProjectsQuery(
    int PageNumber,
    int PageSize,
    ProjectCreatorRole? CreatorRole,
    ProjectStatus? Status
) : IRequest<Result<PaginatedList<InvestmentProjectSummaryDto>>>;