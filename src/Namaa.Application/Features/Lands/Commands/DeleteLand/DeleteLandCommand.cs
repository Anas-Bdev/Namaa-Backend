using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Commands.DeleteLand;
public sealed record DeleteLandCommand(Guid LandId,Guid FarmerId):IRequest<Result<Deleted>>;