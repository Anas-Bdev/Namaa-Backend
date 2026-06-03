using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.MarketPlace;
public sealed class FarmerRating : AuditableEntity
{
    // 1. Traceability (Strictly IDs)
    public Guid OrderId { get; private set; }
    public Guid ReviewerTraderId { get; private set; }
    public Guid FarmerId { get; private set; } // NEW: Added for massive performance!

    // 2. The Review Details
    public int RatingValue { get; private set; }
    public string? Comment { get; private set; }

    private FarmerRating() { }

    private FarmerRating(
        Guid id,
        Guid orderId,
        Guid reviewerTraderId,
        Guid farmerId, // NEW
        int ratingValue,
        string? comment) : base(id)
    {
        OrderId = orderId;
        ReviewerTraderId = reviewerTraderId;
        FarmerId = farmerId;
        RatingValue = ratingValue;
        Comment = comment;
    }

    public static Result<FarmerRating> Create(
        Guid id,
        Guid orderId,
        Guid reviewerTraderId,
        Guid farmerId, // NEW
        int ratingValue,
        string? comment)
    {
        // 1. Validate all relationships to prevent orphan data
        if (id == Guid.Empty) return FarmerRatingErrors.IdRequired;
        if (orderId == Guid.Empty) return FarmerRatingErrors.OrderIdRequired;
        if (reviewerTraderId == Guid.Empty) return FarmerRatingErrors.ReviewerTraderIdRequired;
        if (farmerId == Guid.Empty) return FarmerRatingErrors.FarmerIdRequired;

        // 2. Validate business rules
        if (ratingValue < 1 || ratingValue > 5)
            return FarmerRatingErrors.InvalidRatingValue;

        return new FarmerRating(id, orderId, reviewerTraderId, farmerId, ratingValue, comment);
    }

    // NEW: Allow traders to update their reviews
    public Result<Updated> EditReview(int newRatingValue, string? newComment)
    {
        if (newRatingValue < 1 || newRatingValue > 5)
            return FarmerRatingErrors.InvalidRatingValue;

        RatingValue = newRatingValue;
        Comment = newComment;

        return Result.Updated;
    }
}