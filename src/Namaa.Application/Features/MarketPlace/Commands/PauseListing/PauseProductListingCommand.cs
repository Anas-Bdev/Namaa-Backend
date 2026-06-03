using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.PauseListing;
public sealed record PauseProductListingCommand(Guid ListingId,Guid FarmerId):IRequest<Result<Updated>>;