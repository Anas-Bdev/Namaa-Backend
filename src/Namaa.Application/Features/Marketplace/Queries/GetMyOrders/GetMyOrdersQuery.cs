using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Queries.GetMyOrders;

public sealed record GetMyOrdersQuery(
    Guid TraderId,
    int PageNumber,
    int PageSize
) : IRequest<Result<PaginatedList<OrderDto>>>;