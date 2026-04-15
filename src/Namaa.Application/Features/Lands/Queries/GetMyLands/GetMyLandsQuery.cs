using MediatR;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Queries.GetMyLands;
public sealed record GetMyLandsQuery(Guid FarmerId):IRequest<Result<List<LandDto>>>;