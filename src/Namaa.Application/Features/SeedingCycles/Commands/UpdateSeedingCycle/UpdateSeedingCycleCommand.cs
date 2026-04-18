using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.SeedingCycles.Commands.UpdateSeedingCycle;
public sealed record UpdateSeedingCycleCommand(Guid Id,DateTime StartDate,DateTime EstimatedHarvestDate,double SeedQuantity,double SeedingArea,double ExpectedYield):IRequest<Result<Updated>>;