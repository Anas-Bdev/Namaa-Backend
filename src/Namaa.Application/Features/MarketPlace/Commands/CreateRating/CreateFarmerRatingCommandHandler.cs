using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.MarketPlace;

namespace Namaa.Application.Features.MarketPlace.Commands.CreateRating;

public class CreateFarmerRatingCommandHandler(IAppDbContext context,IIdentityService identityService) : IRequestHandler<CreateFarmerRatingCommand, Result<FarmerRatingDto>>
{
    public async Task<Result<FarmerRatingDto>> Handle(CreateFarmerRatingCommand request, CancellationToken cancellationToken)
    {
        var order=await context.ProductOrders.FirstOrDefaultAsync(x => x.Id==request.OrderId,cancellationToken);
        if(order is null)
        return ApplicationErrors.OrderNotFound;
        if(order.TraderId!=request.TraderId)
        return ApplicationErrors.TraderNotFound;
        if(order.Status!=OrderStatus.Delivered)
        return ApplicationErrors.InvalidRatingState;
        bool alreadyReviewed=await context.FarmerRatings.AnyAsync(x => x.OrderId==request.OrderId,cancellationToken);
        if(alreadyReviewed)
        return ApplicationErrors.OrderAlreadyReviewed;
        var listing=await context.ProductListings.FirstOrDefaultAsync(x => x.Id==order.ProductListingId,cancellationToken);
        if(listing is null)
        return ApplicationErrors.ListingNotFound;
        var ratingId=Guid.NewGuid();
        var ratingResult=FarmerRating.Create(
            id:ratingId,
            orderId:order.Id,
            reviewerTraderId:order.TraderId,
            farmerId:listing.FarmerId,
            ratingValue:request.RatingValue,
            comment:request.Comment
        );
        
        if(ratingResult.IsError)
        return ratingResult.Errors;
        context.FarmerRatings.Add(ratingResult.Value);
        await context.SaveChangesAsync(cancellationToken);
        string reviewerName=await identityService.GetUserNameAsync(order.TraderId.ToString()) ?? string.Empty;
        return ratingResult.Value.ToDto(reviewerName);
    }
}