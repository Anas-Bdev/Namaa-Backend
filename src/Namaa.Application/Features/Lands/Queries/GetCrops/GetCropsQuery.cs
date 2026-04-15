using MediatR;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lands.Queries.GetCrops;
public sealed record GetCropsQuery(Guid LandId):IRequest<Result<List<CropDto>>>;