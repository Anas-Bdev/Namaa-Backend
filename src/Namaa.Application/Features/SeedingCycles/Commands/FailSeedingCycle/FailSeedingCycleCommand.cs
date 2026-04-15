using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Commands.FailSeedingCycle;
public sealed record FailSeedingCycleCommand(Guid Id):IRequest<Result<Updated>>;