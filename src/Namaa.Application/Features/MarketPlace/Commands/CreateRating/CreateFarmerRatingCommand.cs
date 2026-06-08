using MediatR;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.CreateRating;
public sealed record CreateFarmerRatingCommand(
    Guid OrderId,
    int RatingValue,
    string? Comment,
    Guid TraderId
):IRequest<Result<FarmerRatingDto>>;