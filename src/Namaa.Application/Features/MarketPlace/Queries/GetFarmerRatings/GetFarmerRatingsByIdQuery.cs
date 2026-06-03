
using MediatR;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetFarmerRatings;
public sealed record GetFarmerRatingsByIdQuery(
    Guid FarmerId
):IRequest<Result<List<FarmerRatingDto>>>;