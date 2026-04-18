using MediatR;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Queries.GetMySales;

public sealed record GetMySalesQuery(
    Guid FarmerId,
    int PageNumber,
    int PageSize
) : IRequest<Result<PaginatedList<OrderDto>>>;