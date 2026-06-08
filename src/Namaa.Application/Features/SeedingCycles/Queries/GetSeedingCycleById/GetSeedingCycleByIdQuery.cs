using MediatR;
using Namaa.Application.Features.SeedingCycles.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Queries.GetSeedingCycleById;
public sealed record GetSeedingCycleByIdQuery(Guid Id):IRequest<Result<SeedingCycleDto>>;