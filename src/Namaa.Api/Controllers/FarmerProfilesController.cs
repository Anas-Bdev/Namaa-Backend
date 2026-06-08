using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Farmers;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Farmers.Commands.CreateProfile;
using Namaa.Application.Features.Farmers.Commands.UpdateProfile;
using Namaa.Application.Features.Farmers.Queries.GetFarmerProfile;
using Namaa.Application.Features.Farmers.Queries.GetFarmerProfileById;
using Namaa.Application.Features.Farmers.Queries.GetFarmers;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;
[Route("api/farmer-profiles")]
[ApiController]
[Authorize]
public class FarmerProfilesController(ISender sender):ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("profile")]
    [Authorize(Roles =AppRoles.Farmer)]

    public async Task<IActionResult> CreateProfile(CreateFarmerProfileRequest request,CancellationToken cancellationToken)
    {

        var command=new CreateFarmerProfileCommand(
            UserId,
            request.CityId!.Value,
            request.Description,
            request.AddressDetail
        );

        var result=await sender.Send(command,cancellationToken);
        return result.Match(response  => CreatedAtAction(nameof(GetProfile),null,response),errors => this.ToProblem(errors));
    }

    [HttpPut("profile")]
    [Authorize(Roles =AppRoles.Farmer)]

    public async Task<IActionResult> UpdateProfile(UpdateFarmerProfileRequest request,CancellationToken cancellationToken)
    {

        var command=new UpdateFarmerProfileCommand(
            UserId,
            request.CityId!.Value,
            request.Description,
            request.AddressDetail
        );

        var result=await sender.Send(command,cancellationToken);
        return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetFarmers([FromQuery] GetFarmersRequest request,CancellationToken cancellationToken)
    {
        var query=new GetFarmersQuery(request.PageNumber,request.PageSize,request.CityId);
        var result = await sender.Send(query,cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }
   
   [HttpGet("me")]
   [Authorize(Roles =AppRoles.Farmer)]

   public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetFarmerProfileQuery(UserId),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id,CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetFarmerProfileByIdQuery(id),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }   
}