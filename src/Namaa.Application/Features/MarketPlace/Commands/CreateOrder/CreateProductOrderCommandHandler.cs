using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Common.ValueObjects;
using Namaa.Domain.Enums;
using Namaa.Domain.MarketPlace;

namespace Namaa.Application.Features.MarketPlace.Commands.CreateOrder;

public class CreateProductOrderCommandHandler(IAppDbContext context,HybridCache cache) : IRequestHandler<CreateProductOrderCommand, Result<ProductOrderDto>>
{
    public async Task<Result<ProductOrderDto>> Handle(CreateProductOrderCommand request, CancellationToken cancellationToken)
    {
        var listing=await context.ProductListings.FirstOrDefaultAsync(x => x.Id==request.ProductListingId,cancellationToken);
        if(listing is null)
        return ApplicationErrors.ListingNotFound;
        if(listing.Status!=ListingStatus.Active)
        return ApplicationErrors.ListingNotActive;
        var inventoryResult=listing.ReduceQuantity(request.Quantity);
        if(inventoryResult.IsError)
        return inventoryResult.Errors;
        decimal actualPrice=listing.DiscountPrice ?? listing.PricePerUnit;
        var deliveryAddress=new Address(
            request.Governorate,
            request.CityOrVillage,
            request.NeighborhoodOrStreet,
            request.LandMark
        );
        var orderId=Guid.NewGuid();
        var orderResult = ProductOrder.Create(
            id: orderId,
            traderId: request.TraderId,
            productListingId: request.ProductListingId,
            quantity: request.Quantity,
            priceAtPurchase: actualPrice,
            deliveryAddress: deliveryAddress,
            deliveryNotes: request.DeliveryNotes
        );

        if (orderResult.IsError)
         return orderResult.Errors;

        var order = orderResult.Value;
        context.ProductOrders.Add(order);
        await context.SaveChangesAsync(cancellationToken);
        await cache.RemoveByTagAsync("listings",cancellationToken);
        return order.ToDto();
    }
}