using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Domain.Common.Constants;
using Namaa.Application.Features.Experts.Commands.CreateProfile;
using Namaa.Api.Extensions;

namespace Namaa.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles =AppRoles.Expert)]
public class ExpertsController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("upload-cv")]
    public async Task<IActionResult> UploadCv([FromForm]IFormFile file,CancellationToken ct)
    {
        var command=new CreateExpertProfileCommand(UserId,file);
        var result=await sender.Send(command,ct);
        return result.Match(_ => Created(),errors => this.ToProblem(errors));

    }
}