using MediatR;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Common.Models;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetAllListings;

public sealed record GetAllListingsQuery(
string? Category,
string? Location,
decimal? MinPrice,
decimal? MaxPrice,
int PageNumber,
int PageSize
) : ICachedQuery<Result<PaginatedList<ProductListingDto>>>
{
    public string CacheKey => $"listing-{Category ?? "all"}-{Location ?? "all"}-{MinPrice ?? 0}-{MaxPrice ?? 0}-{PageNumber}-{PageSize}";

    public string[] Tags => ["listings"];

    public TimeSpan Expiration => TimeSpan.FromMinutes(5);
}