using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.SeedingCycles;
using Namaa.Api.Extensions;
using Namaa.Application.Features.SeedingCycles.Commands.ActivateSeedingCycle;
using Namaa.Application.Features.SeedingCycles.Commands.CancelSeedingCycle;
using Namaa.Application.Features.SeedingCycles.Commands.FailSeedingCycle;
using Namaa.Application.Features.SeedingCycles.Queries.GetMySeedingCycles;
using Namaa.Application.Features.SeedingCycles.Queries.GetSeedingCycleById;
using Namaa.Application.Features.SeedingCycles.Queries.GetSeedingCyclesByLandId;
using Namaa.Domain.Common.Constants;
namespace Namaa.Api.Controllers;
[Route("/api/seeding-cycles")]
[ApiController]
[Authorize(Roles =$"{AppRoles.Farmer},{AppRoles.Admin},{AppRoles.Investor}")]
public class SeedingCyclesController(ISender sender) : ControllerBase
{

   private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);


   [HttpGet("{id:guid}")] 
   public async Task<IActionResult> GetById(Guid id,CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetSeedingCycleByIdQuery(id),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }

    [HttpGet("land/{landId:guid}")]
    public async Task<IActionResult> GetByLandId(Guid landId,CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetSeedingCyclesByLandIdQuery(landId),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMy(CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetMySeedingCyclesQuery(UserId),cancellationToken);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> Create(CreateSeedingCycleRequest request,CancellationToken cancellationToken)
    {
        var command=request.ToCommand();
        var result=await sender.Send(command,cancellationToken);
        return result.Match(response => CreatedAtAction(nameof(GetById),new {id=response.SeedingCycleId},response),errors => this.ToProblem(errors));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = AppRoles.Farmer)]

    public async Task<IActionResult> Update(Guid id,UpdateSeedingCycleRequest request,CancellationToken cancellationToken)
    {
        var command=request.ToCommand(id);
        var result=await sender.Send(command,cancellationToken);
        return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
    }

    [HttpPut("{id:guid}/activate")]
    [Authorize(Roles = AppRoles.Farmer)]

    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new ActivateSeedingCycleCommand(id), cancellationToken);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("{id:guid}/harvest")]
    [Authorize(Roles = AppRoles.Farmer)]

    public async Task<IActionResult> Harvest(Guid id, HarvestSeedingCycleRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("{id:guid}/cancel")]
    [Authorize(Roles = AppRoles.Farmer)]

    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CancelSeedingCycleCommand(id), cancellationToken);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("{id:guid}/fail")]
    [Authorize(Roles = AppRoles.Farmer)]

    public async Task<IActionResult> Fail(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new FailSeedingCycleCommand(id), cancellationToken);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    

}