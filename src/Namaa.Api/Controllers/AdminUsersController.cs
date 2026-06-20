using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Admin;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Admin.Commands.ActivateUser;
using Namaa.Application.Features.Admin.Commands.SuspendUser;
using Namaa.Application.Features.Admin.Queries.GetAllUsers;
using Namaa.Application.Features.Admin.Queries.GetUserById;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;
[Route("api/admin/users")]
[ApiController]
[Authorize(Roles =AppRoles.Admin)]
public class AdminUsersController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] int pageNumber=1, [FromQuery] int pageSize=10, CancellationToken ct=default)
    {
        var query=new GetAllUsersQuery(pageNumber,pageSize);
        var result=await sender.Send(query,ct);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId,CancellationToken ct)
    {
        var query=new GetUserByIdQuery(userId);
        var result=await sender.Send(query,ct);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpPut("{userId:guid}/activate")]
    public async Task<IActionResult> ActivateUser(Guid userId,CancellationToken ct)
    {
        var command=new ActivateUserCommand(userId);
        var result=await sender.Send(command,ct);
        return result.Match( _ => NoContent(),errors => this.ToProblem(errors) );
    }

    [HttpPut("{userId:guid}/suspend")]
    public async Task<IActionResult> SuspendUser(Guid userId,[FromBody] SuspendUserRequest request,CancellationToken ct)
    {
        var command=new SuspendUserCommand(userId,request.Reason);
        var result=await sender.Send(command,ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }
}