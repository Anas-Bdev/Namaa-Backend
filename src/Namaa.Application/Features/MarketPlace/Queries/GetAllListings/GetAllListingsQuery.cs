using MediatR;
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
):IRequest<Result<PaginatedList<ProductListingDto>>>;