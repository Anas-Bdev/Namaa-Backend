using MediatR;
using Namaa.Application.Features.Lookups.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Lookups.Queries.GetCrops;
public sealed record GetCropsQuery(Guid LandId):IRequest<Result<List<CropDto>>>;