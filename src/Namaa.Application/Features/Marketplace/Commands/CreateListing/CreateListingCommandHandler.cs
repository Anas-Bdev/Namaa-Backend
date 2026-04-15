using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Application.Features.Marketplace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Marketplace;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Application.Features.Marketplace.Commands.CreateListing;

public class CreateListingCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<CreateListingCommand, Result<ProductListingDto>>
{
    public async Task<Result<ProductListingDto>> Handle(
        CreateListingCommand request,
        CancellationToken cancellationToken)
    {
        var seedingCycle = await context.SeedingCycles
            .FirstOrDefaultAsync(s => s.Id == request.SeedingCycleId, cancellationToken);

        if (seedingCycle is null)
            return ApplicationErrors.SeedingCycleNotFound;

        if (seedingCycle.Status != CycleStatus.Completed)
            return ApplicationErrors.SeedingCycleNotCompleted;

        var result = ProductListing.Create(
            Guid.NewGuid(),
            request.SeedingCycleId,
            request.Title,
            request.Description,
            request.Unit,
            request.PricePerUnit,
            request.DiscountPrice,
            request.QuantityAvailable,
            request.ImageUrl,
            request.HarvestDate
        );

        if (result.IsError)
            return result.Errors;

        context.ProductListings.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var farmerName = users.FirstOrDefault(u => u.Id == request.FarmerId)?.FullName ?? string.Empty;

        return result.Value.ToDto(farmerName);
    }
}