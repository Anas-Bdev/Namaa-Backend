using MediatR;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Queries.GetLandsByFarmerId;
public sealed record GetLandsByFarmerIdQuery(Guid FarmerId):IRequest<Result<List<LandDto>>>;