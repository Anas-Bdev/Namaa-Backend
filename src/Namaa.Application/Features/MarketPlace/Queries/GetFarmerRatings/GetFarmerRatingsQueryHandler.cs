using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetFarmerRatings;

public class GetFarmerRatingsByIdQueryHandler(IAppDbContext context,IUserReadRepository userReadRepository) : IRequestHandler<GetFarmerRatingsByIdQuery, Result<List<FarmerRatingDto>>>
{
    public async Task<Result<List<FarmerRatingDto>>> Handle(GetFarmerRatingsByIdQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = userReadRepository.Query(); 

    // 2. Perform the JOIN directly in the DB
    var query = from rating in context.FarmerRatings.AsNoTracking()
                join user in usersQuery on rating.ReviewerTraderId equals user.Id
                where rating.FarmerId == request.FarmerId
                orderby rating.CreatedAtUtc descending
                select new FarmerRatingDto{
                    RatingId=rating.Id,
                    OrderId=rating.OrderId,
                    RatingValue=rating.RatingValue,
                    ReviewerId=rating.ReviewerTraderId,
                    ReviewerName=user.FullName,
                    Comment=rating.Comment,
                    CreatedAt=rating.CreatedAtUtc
                };
        
        var items=await query.ToListAsync(cancellationToken);
        return items;
    }
}