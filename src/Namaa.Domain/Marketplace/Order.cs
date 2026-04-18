using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Domain.Marketplace;

public sealed class Order : AuditableEntity
{
    public Guid TraderId { get; private set; }
    public Guid ListingId { get; private set; }
    public ProductListing? Listing { get; private set; }
    public double Quantity { get; private set; }
    public decimal PriceAtPurchase { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public DateTime? PurchasedAt { get; private set; }

    private Order() { }

    private Order(
        Guid id,
        Guid traderId,
        Guid listingId,
        double quantity,
        decimal priceAtPurchase) : base(id)
    {
        TraderId = traderId;
        ListingId = listingId;
        Quantity = quantity;
        PriceAtPurchase = priceAtPurchase;
    }

    public static Result<Order> Create(
        Guid id,
        Guid traderId,
        Guid listingId,
        double quantity,
        decimal priceAtPurchase)
    {
        if (id == Guid.Empty)
            return OrderErrors.IdRequired;
        if (quantity <= 0)
            return OrderErrors.InvalidQuantity;

        return new Order(id, traderId, listingId, quantity, priceAtPurchase);
    }

    public Result<Updated> Confirm()
    {
        if (Status != OrderStatus.Pending)
            return OrderErrors.InvalidStatusTransition;
        Status = OrderStatus.Confirmed;
        return Result.Updated;
    }

    public Result<Updated> Pay()
    {
        if (Status != OrderStatus.Confirmed)
            return OrderErrors.InvalidStatusTransition;
        Status = OrderStatus.Paid;
        PurchasedAt = DateTime.UtcNow;
        return Result.Updated;
    }

    public Result<Updated> Cancel()
    {
        if (Status == OrderStatus.Paid)
            return OrderErrors.CannotCancelPaidOrder;
        Status = OrderStatus.Cancelled;
        return Result.Updated;
    }
}