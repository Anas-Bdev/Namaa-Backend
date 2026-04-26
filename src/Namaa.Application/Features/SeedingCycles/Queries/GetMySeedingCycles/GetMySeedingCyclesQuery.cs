using MediatR;
using Namaa.Application.Features.SeedingCycles.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Queries.GetMySeedingCycles;
public sealed record GetMySeedingCyclesQuery(Guid FarmerId):IRequest<Result<List<SeedingCycleDto>>>;