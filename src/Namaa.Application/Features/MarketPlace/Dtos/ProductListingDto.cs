using Namaa.Domain.Enums;

namespace Namaa.Application.Features.MarketPlace.Dtos;
public class ProductListingDto
{
    public Guid Id {get;set;}
    public Guid FarmerId {get;set;}
    public Guid? SeedingCycleId {get;set;}
    public int CropId {get;set;}
    public string Title {get;set;}=string.Empty;
    public string? Description {get;set;}
    public string Unit {get;set;}=string.Empty;
    public decimal PricePerUnit {get;set;}
    public decimal? DiscountPrice {get;set;}
    public decimal QuantityAvailable {get;set;}
    public ListingStatus Status {get;set;}
    public bool IsAvailable {get;set;}
    public string? ImageUrl {get;set;}
    public DateTime? HarvestDate {get;set;}
    public string CropName {get;set;}=string.Empty;

}