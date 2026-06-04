using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Weather.Queries.GetLocalWeather;
using Namaa.Application.Features.Weather.Queries.GetWeatherAlerts;
using Namaa.Domain.Common.Constants;
namespace Namaa.Api.Controllers;
[Route("api/weather")]
[ApiController]
[Authorize(Roles =AppRoles.Farmer)] 
public class WeatherController(ISender sender):ControllerBase
{
    [HttpGet("land/{landId:guid}")]
    public async Task<IActionResult> GetForLand(Guid landId, CancellationToken ct)
    {
        var farmerId =  Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await sender.Send(new GetLocalWeatherQuery(farmerId, landId), ct);
        
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }

    [HttpGet("alerts")]
    public async Task<IActionResult> GetAlerts(CancellationToken ct)
    {
        var userId=Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result=await sender.Send(new GetWeatherAlertsQuery(userId),ct);
        
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }
}