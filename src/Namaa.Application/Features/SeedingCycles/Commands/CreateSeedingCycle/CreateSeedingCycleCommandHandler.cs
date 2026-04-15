using MediatR;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Errors;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.SeedingCycles.Dtos;
using Namaa.Application.Features.SeedingCycles.Mappers;
using Namaa.Domain.Common.Results;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Application.Features.SeedingCycles.Commands.CreateSeedingCycle;

public class CreateSeedingCycleCommandHandler(IAppDbContext context) : IRequestHandler<CreateSeedingCycleCommand, Result<SeedingCycleDto>>
{
    public async Task<Result<SeedingCycleDto>> Handle(CreateSeedingCycleCommand request, CancellationToken cancellationToken)
    {
      var landExists=await context.Lands.AnyAsync(l => l.Id==request.LandId,cancellationToken);
      if(!landExists)
      return ApplicationErrors.LandNotFound;

      var seedingCycleResult=SeedingCycle.Create(Guid.NewGuid(),request.LandId,request.CropId,request.StartDate,request.EstimatedHarvestDate,request.InitialStatus,request.SeedQuantity,request.SeedingArea,request.ExpectedYield);

      if(seedingCycleResult.IsError)
      return seedingCycleResult.Errors;

      var cycle=seedingCycleResult.Value;
      context.SeedingCycles.Add(cycle);
      await context.SaveChangesAsync(cancellationToken);
      return cycle.ToDto();
      
      
    }
}