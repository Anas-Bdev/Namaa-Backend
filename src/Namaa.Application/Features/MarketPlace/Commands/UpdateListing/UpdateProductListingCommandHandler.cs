using MediatR;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Domain.Common.Results;
using Namaa.Domain.MarketPlace;

namespace Namaa.Application.Features.MarketPlace.Commands.UpdateListing;

public class UpdateProductListingCommandHandler(IAppDbContext context) : IRequestHandler<UpdateProductListingCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateProductListingCommand request, CancellationToken cancellationToken)
    {
        var listing=await context.ProductListings.FindAsync([request.ListingId],cancellationToken);
        if(listing is null)
        return ApplicationErrors.ListingNotFound;
        if(listing.FarmerId!=request.FarmerId)
        return ApplicationErrors.Forbidden;
        var updateResult=listing.Update(
            title:request.Title,
            description:request.Description,
            cropId:request.CropId,
            unit:request.Unit,
            pricePerUnit:request.PricePerUnit,
            discountPrice:request.DiscountPrice,
            quantityAvailable:request.QuantityAvailable,
            imageUrl:request.ImageUrl,
            harvestDate:request.HarvestDate
        );
        if(updateResult.IsError)
        return updateResult.Errors;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}