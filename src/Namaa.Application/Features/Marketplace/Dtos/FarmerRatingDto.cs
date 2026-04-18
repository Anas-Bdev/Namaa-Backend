namespace Namaa.Application.Features.Marketplace.Dtos;

public class FarmerRatingDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string ReviewerTraderName { get; set; } = string.Empty;
    public int RatingValue { get; set; }
    public string? Comment { get; set; }
}