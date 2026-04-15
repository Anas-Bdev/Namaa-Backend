using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Traders.Queries.GetTraders;

public sealed record GetTradersQuery(
    int PageNumber,
    int PageSize,
    int? CityId
) : IRequest<Result<PaginatedList<TraderSummaryDto>>>;