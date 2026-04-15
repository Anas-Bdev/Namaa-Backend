using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Commands.ActivateSeedingCycle;
public sealed record ActivateSeedingCycleCommand(Guid Id):IRequest<Result<Updated>>;