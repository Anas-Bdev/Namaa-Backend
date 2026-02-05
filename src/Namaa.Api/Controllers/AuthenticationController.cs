using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;
using Namaa.Application.Abstractions.Authentication;
using Namaa.Application.Authentication.Dtos;
namespace Namaa.Api.Controllers;
[Route("api/controller")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpGet]
    public IActionResult Hello()
    {
        return Ok("Hello");
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request,CancellationToken ct=default)
    {
        
        var result =await authService.LoginAsync(request,ct);
        return result.Match<IActionResult>( value => Ok(value),error => BadRequest(error));

    }
    [HttpGet("trusted")]
    [Authorize(Roles ="User")]
    public IActionResult Get()
    {
        return Ok("Trusted Client");
    }
}