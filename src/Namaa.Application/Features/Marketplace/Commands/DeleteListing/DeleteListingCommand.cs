using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Marketplace.Commands.DeleteListing;

public sealed record DeleteListingCommand(
    Guid ListingId,
    Guid FarmerId
) : IRequest<Result<Deleted>>;