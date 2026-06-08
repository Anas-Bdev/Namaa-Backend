
using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetFarmerRatings;

public sealed record GetFarmerRatingsByIdQuery(
    Guid FarmerId
) : ICachedQuery<Result<List<FarmerRatingDto>>>
{
    public string CacheKey => $"farmer-ratings-:{FarmerId}";

    public string[] Tags => [$"farmer:{FarmerId}:ratings"];

    public TimeSpan Expiration => TimeSpan.FromHours(1);
}