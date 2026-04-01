using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Weather.Queries.GetLocalWeather;
using Namaa.Domain.Common.Constants;
namespace Namaa.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles =AppRoles.Farmer)] 
public class WeatherController(ISender sender):ControllerBase
{
    [HttpGet("land/{landId:guid}")]
    public async Task<IActionResult> GetForLand(Guid landId, CancellationToken ct)
    {
        var farmerId =  Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await sender.Send(new GetLocalWeatherQuery(farmerId, landId), ct);
        
        return result.Match(success => Ok(success),errors => this.ToProblem(errors));
    }
}