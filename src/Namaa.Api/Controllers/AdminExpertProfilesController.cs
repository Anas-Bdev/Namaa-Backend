using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Experts;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Experts.Commands.ApproveExpert;
using Namaa.Application.Features.Experts.Commands.RejectExpert;
using Namaa.Application.Features.Experts.Queries.GetPendingExperts;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;
[Route("api/admin/expert-profiles")]
[ApiController]
[Authorize(Roles = AppRoles.Admin)]
public class AdminExpertProfilesController(ISender sender) : ControllerBase
{
    [HttpPut("{expertId:guid}/approve")]
    public async Task<IActionResult> ApproveExpert(Guid expertId,CancellationToken cancellationToken)
    {
        var result=await sender.Send(new ApproveExpertCommand(expertId),cancellationToken);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("{expertId:guid}/reject")]
    public async Task<IActionResult> RejectExpert(Guid expertId,[FromBody]RejectExpertRequest request,CancellationToken cancellationToken)
    {
        var result=await sender.Send(new RejectExpertCommand(expertId,request.Reason),cancellationToken);
        return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
    }

    
    [HttpGet("pending-cv")]
    public async Task<IActionResult> GetPendingExpertsCv(CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetPendingExpertsQuery(),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }
}