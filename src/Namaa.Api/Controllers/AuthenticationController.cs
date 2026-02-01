using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Namaa.Api.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        
    }
}