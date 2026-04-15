using MediatR;
using Namaa.Application.Features.SeedingCycles.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Queries.GetSeedingCyclesByLandId;
public sealed record GetSeedingCyclesByLandIdQuery(Guid LandId):IRequest<Result<List<SeedingCycleDto>>>;