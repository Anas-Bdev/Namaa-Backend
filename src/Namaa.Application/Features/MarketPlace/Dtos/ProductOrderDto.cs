using System;
using Namaa.Domain.Common.ValueObjects;
using Namaa.Domain.Enums;
namespace Namaa.Application.Features.MarketPlace.Dtos;
public class ProductOrderDto
{
    public string OrderNumber {get;set;}=string.Empty;
    public Guid OrderId {get;set;}
    public Guid ProductListingId {get;set;}
    public decimal Quantity {get;set;}
    public decimal UnitPriceAtPurchase {get;set;}
    public decimal TotalPrice {get;set;}
    public OrderStatus Status {get;set;}
    public Address DeliveryAddress {get;set;}=default!;
    public string? DeliveryNotes {get;set;}
    public DateTime? EstimatedArrivalDate {get;set;}
    public DateTimeOffset CreatedAt {get;set;}
}