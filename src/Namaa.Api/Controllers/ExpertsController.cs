using System.Security.Claims;
using MediatR;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Extensions;
using Namaa.Api.Contracts.Requests.Experts;
using Namaa.Api.Contracts.Responses.Common;
using Namaa.Application.Features.Experts.Queries.GetExpertProfile;
using Microsoft.AspNetCore.Authorization;
using Namaa.Domain.Common.Constants;
namespace Namaa.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
=======
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Domain.Common.Constants;
using Namaa.Application.Features.Experts.Commands.CreateProfile;
using Namaa.Api.Extensions;

namespace Namaa.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles =AppRoles.Expert)]
>>>>>>> dev-alaa
public class ExpertsController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

<<<<<<< HEAD
    [HttpPost("register")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Register([FromForm] RegisterExpertRequest request,CancellationToken cancellationToken)
    {
       var command=request.ToCommand();
       var result=await sender.Send(command,cancellationToken);
       return result.Match(_ => Ok(new MessageResponse("Registration successful! Please check your email to verify your account.")),errors => this.ToProblem(errors));
    
    }

    [HttpPut("cv")]
    [Consumes("multipart/form-data")]
    [Authorize(Roles =AppRoles.Expert)]
    public async Task<IActionResult> UpdateCv([FromForm]UpdateCvRequest request,CancellationToken ct)
    {
        var command=request.ToCommand(UserId);
        var result=await sender.Send(command,ct);
        return result.Match(_ => NoContent(),errors => this.ToProblem(errors));

    }

    [HttpPut("profile")]
    [Authorize(Roles =AppRoles.Expert)]

    public async Task<IActionResult> UpdateProfile(UpdateExpertProfileRequest request,CancellationToken cancellationToken)
    {
        var command=request.ToCommand(UserId);
        var result=await sender.Send(command,cancellationToken);
        return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetExperts([FromQuery] GetExpertsRequest request,CancellationToken cancellationToken)
    {
        var query=request.ToQuery();
        var result = await sender.Send(query,cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }
   
   [HttpGet("profile")]
   [Authorize(Roles =AppRoles.Expert)]

   public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetExpertProfileQuery(UserId),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }

    [HttpGet("{id:guid}")]
    
    public async Task<IActionResult> GetById(Guid id,CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetExpertProfileQuery(id),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }
=======
    [HttpPost("upload-cv")]
    public async Task<IActionResult> UploadCv([FromForm]IFormFile file,CancellationToken ct)
    {
        var command=new CreateExpertProfileCommand(UserId,file);
        var result=await sender.Send(command,ct);
        return result.Match(_ => Created(),errors => this.ToProblem(errors));

    }
>>>>>>> dev-alaa
}