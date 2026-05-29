using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Account;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Account.Commands.RemvoeProfileImage;
using Namaa.Application.Features.Account.Commands.UpdateAccountInfo;
using Namaa.Application.Features.Account.Commands.UpdateProfileImage;
using Namaa.Application.Features.Account.Dtos;
using Namaa.Application.Features.Account.Queries.GetCurrentUser;


namespace Namaa.Api.Controllers;
[Route("api/account")]
[ApiController]
[Authorize]
public class AccountController(ISender sender) : ControllerBase
{
    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    
    [HttpGet("me")]
    [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken ct)
    {
        var userId = UserId;
        var query = new GetCurrentUserQuery(userId);
        var result = await sender.Send(query, ct);

        return result.Match(
            response => Ok(response),
            errors => this.ToProblem(errors));
    }

    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordRequest request,
        CancellationToken ct)
    {
        var userId = UserId;

        var command = request.ToCommand(userId);
        var result = await sender.Send(command, ct);

        return result.Match(
            _ => NoContent(),
            errors => this.ToProblem(errors));
    }
    
    [HttpPut("info")]
    public async Task<IActionResult> UpdateAccountInfo([FromBody]UpdateAccountInfoRequest request,CancellationToken ct)
    {
        var userId=UserId;
        var command=new UpdateAccountInfoCommand(
            userId,
            request.FirstName,
            request.LastName,
            request.PhoneNumber
        );
        var result=await sender.Send(command,ct);
        return result.Match(_ => NoContent(),errors => this.ToProblem(errors));

    }

    [HttpPut("profile-image")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateProfileImage([FromForm] IFormFile formFile,CancellationToken ct)
    {
        var userId=UserId;
        var command=new UpdateProfileImageCommand(userId,formFile);
        var result=await sender.Send(command,ct);
        return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
    }

    [HttpDelete("profile-image")]
    public async Task<IActionResult> RemoveProfileImage(CancellationToken ct)
    {
        var userId=UserId;
        var command=new RemoveProfileImageCommand(userId);
        var result=await sender.Send(command,ct);
        return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
    }
}