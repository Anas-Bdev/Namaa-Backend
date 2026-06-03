using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Application.Features.MarketPlace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.MarketPlace;

namespace Namaa.Application.Features.MarketPlace.Commands.CreateListing;

public class CreateProductListingCommandHandler(IAppDbContext context) : IRequestHandler<CreateProductListingCommand, Result<ProductListingDto>>
{
    public async Task<Result<ProductListingDto>> Handle(CreateProductListingCommand request, CancellationToken cancellationToken)
    {
        var cropName=await context.Crops.Where(c => c.Id==request.CropId)
                           .Select(c => c.Name)
                           .FirstOrDefaultAsync(cancellationToken);
        if(string.IsNullOrEmpty(cropName))
        return ApplicationErrors.CropNotFound;
        var listingId=Guid.NewGuid();
        var listingResult=ProductListing.Create(
            id:listingId,
            farmerId:request.FarmerId,
            seedingCycleId:request.SeedingCycleId,
            cropId:request.CropId,
            title:request.Title,
            description:request.Description,
            unit:request.Unit,
            pricePerUnit:request.PricePerUnit,
            discountPrice:request.DiscountPrice,
            quantityAvailable:request.QuantityAvailable,
            imageUrl:request.ImageUrl,
            harvestDate:request.HarvestDate
        );
        if (listingResult.IsError)
        
         return listingResult.Errors;
         context.ProductListings.Add(listingResult.Value);
         await context.SaveChangesAsync(cancellationToken);
        return listingResult.Value.ToDto(cropName);
        
    }
}