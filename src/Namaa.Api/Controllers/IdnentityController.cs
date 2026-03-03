
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Identity;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Identity.Commands.ConfirmEmail;
using Namaa.Application.Features.Identity.Commands.ForgotPassword;
using Namaa.Application.Features.Identity.Commands.Login;
using Namaa.Application.Features.Identity.Commands.LogOut;
using Namaa.Application.Features.Identity.Commands.RefreshTokens;
using Namaa.Application.Features.Identity.Commands.Register;
using Namaa.Application.Features.Identity.Commands.ResendConfirmationEmail;
using Namaa.Application.Features.Identity.Commands.ResetPassword;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Application.Features.Identity.Queries.GetUserInfo;

namespace Namaa.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)] 
public class IdentityController(ISender sender) : ControllerBase
{
  [HttpPost("register")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
  public async Task<IActionResult> Register([FromBody] RegisterRequest request,CancellationToken ct){
    var command=request.ToCommand();
   var result=await sender.Send(command,ct);
   return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
  }

  [HttpGet("confirm-email")]
  [ProducesResponseType(typeof(TokenResponse),StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]

  public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request,CancellationToken ct)
  {
    var command=request.ToCommand();
    var result=await sender.Send(command,ct);
    return result.Match(response => Ok(response),errors => this.ToProblem(errors));
  
  }

  
  [HttpPost("resend-confirm-email")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
   
  public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request,CancellationToken ct)
  {
    var command=request.ToCommand();
    var result=await sender.Send(command,ct);
    return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
  }
    [HttpPost("login")]
   [ProducesResponseType(typeof(TokenResponse),StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
   [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status401Unauthorized)]
   [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]

  public async Task<IActionResult> Login([FromBody] LoginRequest request,CancellationToken ct)
  {
    var command=request.ToCommand();
    var result=await sender.Send(command,ct);
    return result.Match(response => Ok(response),errors => this.ToProblem(errors));
  }
  

  [HttpPost("refresh-token")]

  [ProducesResponseType(typeof(TokenResponse),StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status409Conflict)]
  [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]


  public async Task<IActionResult> RefreshTokens([FromBody] RefreshTokenRequest request,CancellationToken ct)
  {
    var command=request.ToCommand();
    var result=await sender.Send(command,ct);
    return result.Match(response => Ok(response),errors => this.ToProblem(errors));
  }

  [Authorize]
  [HttpPost("logout")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]

  public async Task<IActionResult> LogOut([FromBody] LogOutRequest request,CancellationToken ct)
  {
    var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
    var command=request.ToCommand(userId!);
    var result=await sender.Send(command,ct);
    return result.Match(_ => NoContent(),errors => this.ToProblem(errors));

  }
  [HttpPost("forgot-password")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]

  public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request,CancellationToken ct)
  {
    var command=request.ToCommand();
    var result=await sender.Send(command,ct);
    return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
  }
  [HttpPost("reset-password")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request,CancellationToken ct)
  {
    var command=request.ToCommand();
    var result=await sender.Send(command,ct);
    return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
  }
  [Authorize]
  [HttpGet("me")]
  [ProducesResponseType(typeof(AppUserDto),StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]

  public async Task<IActionResult> GetCurrentUser(CancellationToken ct)
  {
    var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
    var query=new GetUserByIdQuery(userId);
    var result=await sender.Send(query,ct);
    return result.Match(response => Ok(response),errors => this.ToProblem(errors));
  }
  
 
}