using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetOrderById;

public class GetProductOrderByIdQueryHandler(
    IAppDbContext context, 
    IUserReadRepository userReadRepository) 
    : IRequestHandler<GetProductOrderByIdQuery, Result<ProductOrderDto>>
{
    public async Task<Result<ProductOrderDto>> Handle(GetProductOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await context.ProductOrders
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

        if (order is null)
            return ApplicationErrors.OrderNotFound;

        var user = await userReadRepository.GetByIdAsync(order.TraderId, cancellationToken);
        
        var profile = await context.TraderProfiles
            .Include(p => p.Governorate)
            .FirstOrDefaultAsync(p => p.Id == order.TraderId, cancellationToken);

        return new ProductOrderDto
        {
            OrderNumber = order.OrderNumber,
            OrderId = order.Id,
            ProductListingId = order.ProductListingId,
            Quantity = order.Quantity,
            UnitPriceAtPurchase = order.PriceAtPurchase,
            TotalPrice = order.TotalPrice,
            Status = order.Status.ToSpacedName(),
            DeliveryAddress = order.DeliveryAddress,
            DeliveryNotes = order.DeliveryNotes,
            EstimatedArrivalDate = order.EstimatedArrivalDate,
            CreatedAt = order.CreatedAtUtc,
            Trader = new TraderListItemDto
            {
                Id = order.TraderId,
                FullName = user?.FullName ?? "Unknown",
                ProfileImageUrl = user?.ProfileImageUrl,
                Governorate = profile?.Governorate?.Name ?? "N/A",
                BusinessName = profile?.BusinessName ?? "N/A",
                BusinessType = profile?.BusinessType.ToString() ?? "N/A"
            }
        };
    }
}