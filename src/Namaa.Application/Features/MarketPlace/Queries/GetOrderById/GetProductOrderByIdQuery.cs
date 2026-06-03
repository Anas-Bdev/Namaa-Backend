using MediatR;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetOrderById;
public sealed record GetProductOrderByIdQuery(
    Guid OrderId
):IRequest<Result<ProductOrderDto>>;