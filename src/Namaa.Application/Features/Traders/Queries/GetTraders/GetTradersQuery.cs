using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.Traders.Queries.GetTraders;
public sealed record GetTradersQuery(
    int PageNumber, 
    int PageSize,
    int? CityId,
    TraderType? TraderType
) : IRequest<Result<PaginatedList<TraderListItemDto>>>;