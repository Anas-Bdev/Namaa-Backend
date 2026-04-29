using MediatR;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Commands.CreateRating;

public sealed record CreateRatingCommand(
    Guid OrderId,
    Guid TraderId,
    int RatingValue,
    string? Comment
) : IRequest<Result<FarmerRatingDto>>;