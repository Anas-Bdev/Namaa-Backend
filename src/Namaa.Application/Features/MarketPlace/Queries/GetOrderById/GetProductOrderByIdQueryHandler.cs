using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetOrderById;

public class GetProductListingByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductOrderByIdQuery, Result<ProductOrderDto>>
{
    public async Task<Result<ProductOrderDto>> Handle(GetProductOrderByIdQuery request, CancellationToken cancellationToken)
    {
     var order=await context.ProductOrders.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id==request.OrderId);
      

      if(order is null)
      return ApplicationErrors.OrderNotFound;

      return order.ToDto();
    }
}