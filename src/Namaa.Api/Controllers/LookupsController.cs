using MediatR;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Lookups.Queries.GetGovernorates;
using Namaa.Application.Features.Lookups.Queries.GetSoilTypes;
using Namaa.Domain.Enums;
using Namaa.Domain.SeedingCycles;

namespace Namaa.Api.Controllers;
[ApiController]
[Route("api/lookups")]
public class LookupsController(ISender sender) : ControllerBase
{
    [HttpGet("water-sources")]
    public IActionResult GetWaterSources() => Ok(GetEnumList<WaterSourceType>());

    [HttpGet("trader-types")]
    public IActionResult GetTraderTypes() => Ok(GetEnumList<TraderType>());

    [HttpGet("investor-types")]
    public IActionResult GetInvestorTypes() => Ok(GetEnumList<InvestorType>());

    [HttpGet("water-availabilities")]
    public IActionResult GetWaterAvailabilities() => Ok(GetEnumList<WaterAvailability>());

    [HttpGet("environment-types")]
    public IActionResult GetEnvironmentTypes() => Ok(GetEnumList<EnvironmentType>());

    [HttpGet("irrigation-methods")]
    public IActionResult GetIrrigationMethods() => Ok(GetEnumList<IrrigationMethod>());

    // --- Seeding Cycle Status Lookups ---

    [HttpGet("seeding-cycle-statuses")]
    public IActionResult GetSeedingCycleStatuses()
    {
        return Ok(GetEnumList<CycleStatus>());
    }

    [HttpGet("expert-specializations")]
   public IActionResult GetExpertSpecializations() => Ok(GetEnumList<ExpertSpecialization>());

    [HttpGet("seeding-cycle-initial-statuses")]
    public IActionResult GetSeedingCycleInitialStatuses()
    {
        var allowed = new[] { CycleStatus.Planned, CycleStatus.Active };
        
        var result = allowed.Select(e => new 
        { 
            Id = Convert.ToInt32(e), 
            Name = e.ToString() 
        });

        return Ok(result);
    }


    [HttpGet("soil-types")]
    public async Task<IActionResult> GetSoilTypes()
    {
        var result = await sender.Send(new GetSoilTypesQuery());
        return result.Match(success => Ok(success), errors => this.ToProblem(errors));
    }

    [HttpGet("governorates")]
    public async Task<IActionResult> GetGovernorates()
    {
        var result = await sender.Send(new GetGovernoratesQuery());
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    private static IEnumerable<object> GetEnumList<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e => new 
            { 
                Id = Convert.ToInt32(e), 
                Name = e.ToString() 
            });
    }
}