using MediatR;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetTraderOrders;
public sealed record GetTraderProductOrdersQuery(
    Guid TraderId
):IRequest<Result<List<ProductOrderDto>>>;