using MediatR;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetFarmerSales;
public sealed record GetFarmerProductOrderSalesQuery(
    Guid FarmerId
):IRequest<Result<List<ProductOrderDto>>>;