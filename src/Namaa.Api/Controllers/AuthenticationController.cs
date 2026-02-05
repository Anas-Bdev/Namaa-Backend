using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Extensions;
using Namaa.Application.Abstractions.Authentication;
using Namaa.Application.Authentication.Dtos;
using Namaa.Domain.Common.Results;
namespace Namaa.Api.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest request)
    {
        var result=await authService.LoginAsync(request);
        return result.Match<IActionResult>(success => Ok(success), error => this.ToProblem(error));
    }
}