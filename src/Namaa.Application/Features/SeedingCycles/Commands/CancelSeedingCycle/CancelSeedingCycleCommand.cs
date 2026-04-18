using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Commands.CancelSeedingCycle;
public sealed record CancelSeedingCycleCommand(Guid Id):IRequest<Result<Updated>>;