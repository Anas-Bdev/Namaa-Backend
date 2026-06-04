using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Common.ValueObjects;
using Namaa.Domain.Enums;

namespace Namaa.Domain.MarketPlace;
public sealed class ProductOrder : AuditableEntity
{
    // 1. Relationships (Strictly IDs)
    public Guid TraderId { get; private set; }
    public Guid ProductListingId { get; private set; } 
    public ProductListing? ProductListing {get;private set;}

    // 2. Financial Snapshot
    public decimal Quantity { get; private set; } 
    public decimal PriceAtPurchase { get; private set; }
    public decimal TotalPrice => Quantity * PriceAtPurchase; 

    // 3. Lifecycle & Payment Tracking
    public OrderStatus Status { get; private set; }
    public DateTime? PurchasedAt { get; private set; }
    public DateTime? EstimatedArrivalDate { get; private set; }
    
    // 4. Logistics 
    public string? DeliveryNotes { get; private set; } // The beautiful touch!
    public Address DeliveryAddress {get;private set;}
     
     #pragma warning disable CS8618
    private ProductOrder() { }
    #pragma warning restore CS8618
    private ProductOrder(
        Guid id,
        Guid traderId,
        Guid productListingId,
        decimal quantity,
        decimal priceAtPurchase,
        Address deliveryAddress,
        string? deliveryNotes) : base(id) // Added deliveryNotes
    {
        TraderId = traderId;
        ProductListingId = productListingId;
        Quantity = quantity;
        PriceAtPurchase = priceAtPurchase;
        DeliveryNotes = deliveryNotes;
        DeliveryAddress=deliveryAddress;
        
        // Always starts as Pending
        Status = OrderStatus.Pending;
    }

    public static Result<ProductOrder> Create(
        Guid id,
        Guid traderId,
        Guid productListingId,
        decimal quantity,
        decimal priceAtPurchase,
        Address deliveryAddress,
        string? deliveryNotes = null) // Made optional so the trader doesn't have to leave a note
    {
        if (id == Guid.Empty) return ProductOrderErrors.IdRequired;
        if (traderId == Guid.Empty) return ProductOrderErrors.TraderIdRequired;
        if (productListingId == Guid.Empty) return ProductOrderErrors.ProductListingIdRequired;
        if(deliveryAddress is null) return ProductOrderErrors.AddressRequired;
        
        if (quantity <= 0) return ProductOrderErrors.InvalidQuantity;
        if (priceAtPurchase <= 0) return ProductOrderErrors.InvalidPrice;

        return new ProductOrder(id, traderId, productListingId, quantity, priceAtPurchase,deliveryAddress, deliveryNotes);
    }

    // --- The Complete State Machine ---

    public Result<Updated> Confirm()
    {
        if (Status != OrderStatus.Pending)
            return ProductOrderErrors.InvalidStatusTransition;
            
        Status = OrderStatus.Confirmed;
        return Result.Updated;
    }

    public Result<Updated> Pay()
    {
        if (Status != OrderStatus.Confirmed)
            return ProductOrderErrors.InvalidStatusTransition;

        Status = OrderStatus.Paid;
        PurchasedAt = DateTime.UtcNow; 
        
        return Result.Updated;
    }

    public Result<Updated> Ship(DateTime estimatedArrival)
    {
       if(Status!=OrderStatus.Paid) 
       return ProductOrderErrors.InvalidStatusTransition;

       if(estimatedArrival < DateTime.UtcNow)
       return ProductOrderErrors.InvalidArrivalDate;

       Status=OrderStatus.Shipped;
       EstimatedArrivalDate=estimatedArrival;
       return Result.Updated;
    }





    // NEW: The physical handover step
    public Result<Updated> Deliver()
    {
        if (Status != OrderStatus.Shipped)
        return ProductOrderErrors.InvalidStatusTransition;

        Status = OrderStatus.Delivered;
        return Result.Updated;
    }

    public Result<Updated> Cancel()
    {
        // Prevent cancellation if money has changed hands or goods have arrived
        if (Status == OrderStatus.Paid || Status == OrderStatus.Delivered)
            return ProductOrderErrors.CannotCancelPaidOrder;
            
        Status = OrderStatus.Cancelled;
        return Result.Updated;
    }
}