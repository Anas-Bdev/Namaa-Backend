using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.MarketPlace;

namespace Namaa.Application.Features.MarketPlace.Commands.CreateRating;

public class CreateFarmerRatingCommandHandler(
    IAppDbContext context,
    HybridCache cache,
    IAiConsultantService aiConsultantService) 
    : IRequestHandler<CreateFarmerRatingCommand, Result<FarmerRatingDto>>
{
    public async Task<Result<FarmerRatingDto>> Handle(CreateFarmerRatingCommand request, CancellationToken cancellationToken)
    {
        var order = await context.ProductOrders
            .Include(x => x.ProductListing) 
            .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

        if (order is null) return ApplicationErrors.OrderNotFound;
        if (order.TraderId != request.TraderId) return ApplicationErrors.TraderNotFound;
        if (order.Status != OrderStatus.Delivered) return ApplicationErrors.InvalidRatingState;
        
        if (order.ProductListing is null) return ApplicationErrors.ListingNotFound;

        bool alreadyReviewed = await context.FarmerRatings.AnyAsync(x => x.OrderId == request.OrderId, cancellationToken);
        if (alreadyReviewed) return ApplicationErrors.OrderAlreadyReviewed;

        var ratingId = Guid.NewGuid();
        var ratingResult = FarmerRating.Create(
            id: ratingId,
            orderId: order.Id,
            reviewerTraderId: order.TraderId,
            farmerId: order.ProductListing.FarmerId, 
            ratingValue: request.RatingValue,
            comment: request.Comment
        );

        if (ratingResult.IsError) return ratingResult.Errors;

        context.FarmerRatings.Add(ratingResult.Value);

        var reviewComments = await context.FarmerRatings
            .Where(r => r.FarmerId == order.ProductListing.FarmerId && !string.IsNullOrWhiteSpace(r.Comment))
            .Select(r => r.Comment!)
            .ToListAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(request.Comment))
        {
            reviewComments.Add(request.Comment);
        }

        if (reviewComments.Count >= 3)
        {
            var aiSummary = await aiConsultantService.GenerateReviewSummaryAsync(reviewComments, cancellationToken);
            
            var farmerProfile = await context.FarmerProfiles.FindAsync([order.ProductListing.FarmerId], cancellationToken);
            if (farmerProfile is not null)
            {
                farmerProfile.UpdateAiSummary(aiSummary);
            }
        }

        await context.SaveChangesAsync(cancellationToken);

        
        await cache.RemoveByTagAsync($"farmer:{order.ProductListing.FarmerId}:ratings", cancellationToken);
        
        return ratingResult.Value.ToDto(order.TraderId.ToString());
    }
}