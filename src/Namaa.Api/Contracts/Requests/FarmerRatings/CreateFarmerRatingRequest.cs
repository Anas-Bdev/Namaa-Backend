using System.ComponentModel.DataAnnotations;

namespace Namaa.Api.Contracts.Requests.FarmerRatings;
public class CreateFarmerRatingRequest
{
    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int RatingValue { get; set; }

    [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
    public string? Comment { get; set; }
}