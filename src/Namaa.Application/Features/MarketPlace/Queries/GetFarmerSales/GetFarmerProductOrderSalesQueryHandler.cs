using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Application.Features.Traders.Dtos;
using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetFarmerSales;

public class GetFarmerProductOrderSalesQueryHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository) 
    : IRequestHandler<GetFarmerProductOrderSalesQuery, Result<List<ProductOrderDto>>>
{
    public async Task<Result<List<ProductOrderDto>>> Handle(GetFarmerProductOrderSalesQuery request, CancellationToken cancellationToken)
    {
        var orders = await context.ProductOrders
            .AsNoTracking()
            .Where(o => o.ProductListing!.FarmerId == request.FarmerId)
            .ToListAsync(cancellationToken);

        var resultDtos = new List<ProductOrderDto>();

        foreach (var order in orders)
        {
            var user = await userReadRepository.GetByIdAsync(order.TraderId, cancellationToken);
            
            var profile = await context.TraderProfiles
                .Include(p => p.Governorate)
                .FirstOrDefaultAsync(p => p.Id == order.TraderId, cancellationToken);
            
            resultDtos.Add(new ProductOrderDto
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
            });
        }

        return resultDtos;
    }
}