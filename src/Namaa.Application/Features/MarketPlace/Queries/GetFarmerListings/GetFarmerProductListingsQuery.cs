using MediatR;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetFarmerListings;
public sealed record GetFarmerProductListingsQuery(
    Guid FarmerId
):IRequest<Result<List<ProductListingDto>>>;