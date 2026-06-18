namespace Namaa.Application.Features.MarketPlace.Dtos;
public class FarmerRatingsSummaryDto
{
    public double AverageRating {get;set;}
    public int TotalReviews {get;set;}
    public string? AiSummary {get;set;}
    public List<FarmerRatingDto> Reviews {get;set;}=[];
}