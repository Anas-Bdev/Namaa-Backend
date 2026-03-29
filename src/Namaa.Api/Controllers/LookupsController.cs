
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Namaa.Domain.Enums;

namespace Namaa.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LookupsController(ISender sender) : ControllerBase
{
  [HttpGet("water-sources")]
    public IActionResult GetWaterSources()
    {
        return Ok(GetEnumList<WaterSourceType>());
    }

    [HttpGet("water-availabilities")]
    public IActionResult GetWaterAvailabilities()
    {
        return Ok(GetEnumList<WaterAvailability>());
    }

    [HttpGet("environment-types")]
    public IActionResult GetEnvironmentTypes()
    {
        return Ok(GetEnumList<EnvironmentType>());
    }

    [HttpGet("irrigation-methods")]
    public IActionResult GetIrrigationMethods()
    {
        return Ok(GetEnumList<IrrigationMethod>());
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

