using MediatR;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;

namespace Namaa.Application.Features.SeedingCycles.Commands.UpdateSeedingCycle;
public sealed record UpdateSeedingCycleCommand(Guid Id,DateTime StartDate,DateTime EstimatedHarvestDate,double SeedQuantity,double SeedingArea,double ExpectedYield,EnvironmentType EnvironmentType):IRequest<Result<Updated>>;