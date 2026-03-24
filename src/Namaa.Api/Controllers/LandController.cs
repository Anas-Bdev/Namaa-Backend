using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Lands;
using Namaa.Api.Contracts.Responses.Common;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Lands.Commands.DeleteLand;
using Namaa.Application.Features.Lands.Dtos;
using Namaa.Application.Features.Lands.Queries.GetLandById;
using Namaa.Application.Features.Lands.Queries.GetLandsByFarmerId;
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
    [HttpPost]
    [ProducesResponseType(typeof(LandDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLand([FromBody] CreateLandRequest request, CancellationToken ct)
    {
        var farmerIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var farmerId = Guid.Parse(farmerIdStr!);

        var command = request.ToCommand(farmerId);

        var result = await sender.Send(command, ct);
        
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<LandDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyLands(CancellationToken ct)
    {
        var farmerIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var query = new GetLandsByFarmerIdQuery(Guid.Parse(farmerIdStr!));
        
        var result = await sender.Send(query, ct);
        
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LandDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLandById([FromRoute] Guid id, CancellationToken ct)
    {
        var query = new GetLandByIdQuery(id);
        var result = await sender.Send(query, ct);
        
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLand([FromRoute] Guid id, [FromBody] UpdateLandRequest request, CancellationToken ct)
    {
     
        var command=request.ToCommand(id);
        var result = await sender.Send(command, ct);
        
        return result.Match(
            _ => Ok(new MessageResponse("Land updated successfully.")), 
            errors => this.ToProblem(errors));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLand([FromRoute] Guid id, CancellationToken ct)
    {
        var command = new DeleteLandCommand(id);
        var result = await sender.Send(command, ct);
        
        return result.Match(
            _ => Ok(new MessageResponse("Land deleted successfully.")), 
            errors => this.ToProblem(errors));
    }
}