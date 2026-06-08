using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.ArchiveListing;
public sealed record ArchiveProductListingCommand(Guid ListingId,Guid FarmerId):IRequest<Result<Updated>>;