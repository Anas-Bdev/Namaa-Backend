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
    
}