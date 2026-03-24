using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Commands.DeleteLand;
public sealed record DeleteLandCommand(Guid LandId):IRequest<Result<Deleted>>;