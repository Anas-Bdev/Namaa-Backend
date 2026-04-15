using MediatR;
using Namaa.Application.Features.Marketplace.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Queries.GetListingById;

public sealed record GetListingByIdQuery(Guid ListingId) : IRequest<Result<ProductListingDto>>;