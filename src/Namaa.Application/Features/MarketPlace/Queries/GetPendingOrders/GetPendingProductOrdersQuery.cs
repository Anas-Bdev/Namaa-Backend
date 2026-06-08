using MediatR;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetPendingOrders;
public sealed record GetPendingProductOrdersQuery(
    Guid FarmerId
):IRequest<Result<List<ProductOrderDto>>>;