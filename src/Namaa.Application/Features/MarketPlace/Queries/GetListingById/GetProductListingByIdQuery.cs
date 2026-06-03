using MediatR;
using Namaa.Application.Features.MarketPlace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Queries.GetListingById;
public sealed record GetProductListingByIdQuery(Guid Id):IRequest<Result<ProductListingDto>>;