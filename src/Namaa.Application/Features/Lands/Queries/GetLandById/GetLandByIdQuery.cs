using System.ComponentModel.Design;
using MediatR;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Domain.Common.Results;
namespace Namaa.Application.Features.Lands.Queries.GetLandById;
public sealed record GetLandByIdQuery(Guid LandId):IRequest<Result<LandDto>>;