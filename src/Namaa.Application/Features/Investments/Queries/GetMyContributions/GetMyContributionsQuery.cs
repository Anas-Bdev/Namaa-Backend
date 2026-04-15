using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Investments.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Investments.Queries.GetMyContributions;

public sealed record GetMyContributionsQuery(
    Guid UserId,
    int PageNumber,
    int PageSize
) : IRequest<Result<PaginatedList<ContributionDto>>>;