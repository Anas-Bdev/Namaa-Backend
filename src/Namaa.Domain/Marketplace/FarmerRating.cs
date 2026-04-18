using Namaa.Domain.Common;
using Namaa.Domain.Common.Results;

namespace Namaa.Domain.Marketplace;

public sealed class FarmerRating : AuditableEntity
{
    public Guid OrderId { get; private set; }
    public Guid ReviewerTraderId { get; private set; }
    public int RatingValue { get; private set; }
    public string? Comment { get; private set; }

    private FarmerRating() { }

    private FarmerRating(
        Guid id,
        Guid orderId,
        Guid reviewerTraderId,
        int ratingValue,
        string? comment) : base(id)
    {
        OrderId = orderId;
        ReviewerTraderId = reviewerTraderId;
        RatingValue = ratingValue;
        Comment = comment;
    }

    public static Result<FarmerRating> Create(
        Guid id,
        Guid orderId,
        Guid reviewerTraderId,
        int ratingValue,
        string? comment)
    {
        if (id == Guid.Empty)
            return FarmerRatingErrors.IdRequired;
        if (ratingValue < 1 || ratingValue > 5)
            return FarmerRatingErrors.InvalidRatingValue;

        return new FarmerRating(id, orderId, reviewerTraderId, ratingValue, comment);
    }
}
