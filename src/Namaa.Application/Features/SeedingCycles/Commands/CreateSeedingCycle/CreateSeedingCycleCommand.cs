using MediatR;
using Namaa.Application.Features.SeedingCycles.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Enums;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Application.Features.SeedingCycles.Commands.CreateSeedingCycle;
public sealed record CreateSeedingCycleCommand(Guid LandId,string CropName,DateTime StartDate,DateTime EstimatedHarvestDate,CycleStatus InitialStatus,double SeedQuantity,double SeedingArea,double ExpectedYield,EnvironmentType EnvironmentType):IRequest<Result<SeedingCycleDto>>;