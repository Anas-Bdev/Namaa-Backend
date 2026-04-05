using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Lands;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Lands.Commands.DeleteLand;
using Namaa.Application.Features.Lands.Queries.GetLandById;
using Namaa.Application.Features.Lands.Queries.GetMyLands;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles =AppRoles.Farmer)] 
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)] 
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public class LandsController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    public async Task<IActionResult> CreateLand([FromBody] CreateLandRequest request, CancellationToken ct)
    {
        var command = request.ToCommand(UserId); 
        var result = await sender.Send(command, ct);
        return result.Match(response => CreatedAtAction(nameof(GetLandById),new {id=response.LandId},response), errors => this.ToProblem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetMyLands(CancellationToken ct)
    {
        var query = new GetMyLandsQuery(UserId);
        var result = await sender.Send(query, ct);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetLandById([FromRoute] Guid id, CancellationToken ct)
    {
        var query = new GetLandByIdQuery(id, UserId);
        var result = await sender.Send(query, ct);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateLand([FromRoute] Guid id, [FromBody] UpdateLandRequest request, CancellationToken ct)
    {
        var command = request.ToCommand(id, UserId); 
        var result = await sender.Send(command, ct);
        return result.Match(
            _ => NoContent(), 
            errors => this.ToProblem(errors));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteLand([FromRoute] Guid id, CancellationToken ct)
    {
        var command = new DeleteLandCommand(id, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(
            _ => NoContent(), 
            errors => this.ToProblem(errors));
    }
}