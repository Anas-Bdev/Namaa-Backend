using MediatR;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Domain.Common.Results;
namespace Namaa.Application.Features.Lands.Queries.GetLandById;
public sealed record GetLandByIdQuery(Guid LandId,Guid FarmerId):IRequest<Result<LandDto>>;