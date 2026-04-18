using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Application.Features.Marketplace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Marketplace;

namespace Namaa.Application.Features.Marketplace.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var listing = await context.ProductListings
            .FirstOrDefaultAsync(l => l.Id == request.ListingId, cancellationToken);

        if (listing is null)
            return ApplicationErrors.ListingNotFound;

        if (request.Quantity > listing.QuantityAvailable)
            return OrderErrors.InsufficientQuantity;

        var price = listing.DiscountPrice ?? listing.PricePerUnit;

        var result = Order.Create(
            Guid.NewGuid(),
            request.TraderId,
            request.ListingId,
            request.Quantity,
            price
        );

        if (result.IsError)
            return result.Errors;

        context.Orders.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var traderName = users.FirstOrDefault(u => u.Id == request.TraderId)?.FullName ?? string.Empty;

        return result.Value.ToDto(traderName, listing.Title);
    }
}