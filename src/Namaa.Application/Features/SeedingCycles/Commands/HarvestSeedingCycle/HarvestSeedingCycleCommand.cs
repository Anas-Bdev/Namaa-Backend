using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Commands.HarvestSeedingCycle;
public sealed record HarvestSeedingCycleCommand(Guid Id,DateTime ActualHarvestDate,double ActualYield):IRequest<Result<Updated>>;