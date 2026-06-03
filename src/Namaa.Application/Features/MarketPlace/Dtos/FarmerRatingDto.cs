namespace Namaa.Application.Features.MarketPlace.Dtos;
public class FarmerRatingDto
{
    public  Guid RatingId {get;set;}
    public Guid OrderId {get;set;}
    public Guid ReviewerId {get;set;}
    public string ReviewerName {get;set;}=string.Empty;
    public int RatingValue {get;set;}
    public string? Comment {get;set;}
    public DateTimeOffset CreatedAt {get;set;}

}