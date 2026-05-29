using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Traders;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Traders.Commands.CreateProfile;
using Namaa.Application.Features.Traders.Commands.UpdateProfile;
using Namaa.Application.Features.Traders.Queries.GetTraderProfile;
using Namaa.Application.Features.Traders.Queries.GetTraderProfileById;
using Namaa.Application.Features.Traders.Queries.GetTraders;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;
[Route("api/trader-profiles")]
[ApiController]
[Authorize]
public class TraderProfilesController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("profile")]
    [Authorize(Roles =AppRoles.Trader)]

    public async Task<IActionResult> CreateProfile(CreateTraderProfileRequest request,CancellationToken cancellationToken)
    {

        var command=new CreateTraderProfileCommand(
            UserId,
            request.BusinessName,
            request.TraderType!.Value,
            request.CityId!.Value,
            request.AddressDetail
        );

        var result=await sender.Send(command,cancellationToken);
        return result.Match(response  => CreatedAtAction(nameof(GetProfile),null,response),errors => this.ToProblem(errors));
    }

    [HttpPut("profile")]
    [Authorize(Roles =AppRoles.Trader)]

    public async Task<IActionResult> UpdateProfile(UpdateTraderProfileRequest request,CancellationToken cancellationToken)
    {

        var command=new UpdateTraderProfileCommand(
            UserId,
            request.BusinessName,
            request.TraderType!.Value,
            request.CityId!.Value,
            request.AddressDetail
        );

        var result=await sender.Send(command,cancellationToken);
        return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
    }

    [HttpGet]
    [Authorize(Roles =$"{AppRoles.Admin}, {AppRoles.Farmer}")]
    public async Task<IActionResult> GetTraders([FromQuery] GetTradersRequest request,CancellationToken cancellationToken)
    {
        var query=new GetTradersQuery(request.PageNumber,request.PageSize,request.CityId,request.TraderType);
        var result = await sender.Send(query,cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }
   
   [HttpGet("me")]
   [Authorize(Roles =AppRoles.Trader)]

   public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetTraderProfileQuery(UserId),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles =$"{AppRoles.Admin},{AppRoles.Farmer}")]
    
    public async Task<IActionResult> GetById(Guid id,CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetTraderProfileByIdQuery(id),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }   
}