using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Application.Features.Marketplace.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.Marketplace;

namespace Namaa.Application.Features.Marketplace.Commands.CreateRating;

public class CreateRatingCommandHandler(
    IAppDbContext context,
    IUserReadRepository userReadRepository)
    : IRequestHandler<CreateRatingCommand, Result<FarmerRatingDto>>
{
    public async Task<Result<FarmerRatingDto>> Handle(
        CreateRatingCommand request,
        CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

        if (order is null)
            return ApplicationErrors.OrderNotFound;

        if (order.TraderId != request.TraderId)
            return ApplicationErrors.Forbidden;

        if (order.Status != OrderStatus.Paid)
            return ApplicationErrors.OrderNotPaid;

        var alreadyRated = await context.FarmerRatings
            .AnyAsync(r => r.OrderId == request.OrderId, cancellationToken);

        if (alreadyRated)
            return FarmerRatingErrors.AlreadyRated;

        var result = FarmerRating.Create(
            Guid.NewGuid(),
            request.OrderId,
            request.TraderId,
            request.RatingValue,
            request.Comment
        );

        if (result.IsError)
            return result.Errors;

        context.FarmerRatings.Add(result.Value);
        await context.SaveChangesAsync(cancellationToken);

        var users = await userReadRepository.Query().ToListAsync(cancellationToken);
        var traderName = users.FirstOrDefault(u => u.Id == request.TraderId)?.FullName ?? string.Empty;

        return result.Value.ToDto(traderName);
    }
}