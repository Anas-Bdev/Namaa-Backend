using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Extensions;
using Namaa.Api.Contracts.Requests.Experts;
using Namaa.Application.Features.Experts.Queries.GetExpertProfile;
using Microsoft.AspNetCore.Authorization;
using Namaa.Domain.Common.Constants;
using Namaa.Application.Features.Experts.Commands.UpdateCv;
using Namaa.Application.Features.Experts.Queries.GetExperts;
using Namaa.Application.Features.Experts.Commands.UpdateProfile;
using Namaa.Application.Features.Experts.Queries.GetExpertProfileById;
using Namaa.Application.Features.Experts.Queries.GetPendingExperts;
namespace Namaa.Api.Controllers;
[Route("api/expert-profiles")]
[ApiController]
[Authorize]
public class ExpertProfilesController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);


    [HttpPut("cv")]
    [Consumes("multipart/form-data")]
    [Authorize(Roles =AppRoles.Expert)]
    public async Task<IActionResult> UpdateCv([FromForm]IFormFile formFile,CancellationToken ct)
    {
        var command=new UpdateExpertCvCommand(UserId,formFile);
        var result=await sender.Send(command,ct);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));

    }

    [HttpPut("profile")]
    [Authorize(Roles =AppRoles.Expert)]

    public async Task<IActionResult> UpdateProfile(UpdateExpertProfileRequest request,CancellationToken cancellationToken)
    {

        var availabilities=request.Availabilities.ConvertAll(x => new UpdateExpertAvailabilityCommand(x.Day!.Value,x.StartTime!.Value,x.EndTime!.Value));
        var command=new UpdateExpertProfileCommand(
            UserId,
            request.Specialization!.Value,
            request.YearsOfExperience!.Value,
            request.CityId!.Value,
            request.AddressDetail,
            request.CanVisitOnSite!.Value,
           availabilities
        );

        var result=await sender.Send(command,cancellationToken);
        return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
    }

    [HttpGet]
    [Authorize(Roles =$"{AppRoles.Admin}, {AppRoles.Farmer}")]
    public async Task<IActionResult> GetExperts([FromQuery] GetExpertsRequest request,CancellationToken cancellationToken)
    {
        var query=new GetExpertsQuery(request.PageNumber,request.PageSize,request.CityId,request.Specialization);
        var result = await sender.Send(query,cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }
   
   [HttpGet("me")]
   [Authorize(Roles =AppRoles.Expert)]

   public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetExpertProfileQuery(UserId),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles =$"{AppRoles.Admin}, {AppRoles.Farmer}")]
    public async Task<IActionResult> GetById(Guid id,CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetExpertProfileByIdQuery(id),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }

    
}