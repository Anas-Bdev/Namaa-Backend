using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.MarketPlace.Commands.ResumeListing;
public sealed record ResumeProductListingCommand(Guid ListingId,Guid FarmerId):IRequest<Result<Updated>>;