using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetFarmerRatings;

public class GetFarmerRatingsByIdQueryHandler(IAppDbContext context, IUserReadRepository userReadRepository) 
    : IRequestHandler<GetFarmerRatingsByIdQuery, Result<FarmerRatingsSummaryDto>>
{
    public async Task<Result<FarmerRatingsSummaryDto>> Handle(GetFarmerRatingsByIdQuery request, CancellationToken cancellationToken)
    {
        bool farmerExists = await context.FarmerProfiles
            .AnyAsync(f => f.Id == request.FarmerId, cancellationToken);
            
        if (!farmerExists)
        {
            return ApplicationErrors.FarmerNotFound; 
        }

        var farmerProfile = await context.FarmerProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == request.FarmerId, cancellationToken);
            
        var usersQuery = userReadRepository.Query(); 
        
        var query = from rating in context.FarmerRatings.AsNoTracking()
                    join user in usersQuery on rating.ReviewerTraderId equals user.Id
                    where rating.FarmerId == request.FarmerId
                    orderby rating.CreatedAtUtc descending
                    select new FarmerRatingDto {
                        RatingId = rating.Id,
                        OrderId = rating.OrderId,
                        RatingValue = rating.RatingValue,
                        ReviewerId = rating.ReviewerTraderId,
                        ReviewerName = user.FullName,
                        Comment = rating.Comment,
                        CreatedAt = rating.CreatedAtUtc
                    };
        
        var items = await query.ToListAsync(cancellationToken);

        var totalReviews = items.Count;
        var averageRating = totalReviews > 0 ? items.Average(x => x.RatingValue) : 0;

        return new FarmerRatingsSummaryDto{
            AverageRating= Math.Round(averageRating, 1),
            TotalReviews=totalReviews,
            AiSummary= farmerProfile?.AiReviewSummary,
            Reviews=items
        };
        
    }
}