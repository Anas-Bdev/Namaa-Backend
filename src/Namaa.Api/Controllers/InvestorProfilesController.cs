using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Investors;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Investors.Commands.CreateProfile;
using Namaa.Application.Features.Investors.Commands.UpdateProfile;
using Namaa.Application.Features.Investors.Queries.GetInvestorProfile;
using Namaa.Application.Features.Investors.Queries.GetInvestorProfileById;
using Namaa.Application.Features.Investors.Queries.GetInvestors;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;
[Route("api/investor-profiles")]
[ApiController]
[Authorize]
public class InvestorProfilesController(ISender sender):ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("profile")]
    [Authorize(Roles =AppRoles.Investor)]

    public async Task<IActionResult> CreateProfile(CreateInvestorProfileRequest request,CancellationToken cancellationToken)
    {

        var command=new CreateInvestorProfileCommand(
            UserId,
            request.InvestorType!.Value,
            request.OrganizationName,
            request.CityId!.Value,
            request.AddressDetail
        );

        var result=await sender.Send(command,cancellationToken);
        return result.Match(response  => CreatedAtAction(nameof(GetProfile),null,response),errors => this.ToProblem(errors));
    }

    [HttpPut("profile")]
    [Authorize(Roles =AppRoles.Investor)]

    public async Task<IActionResult> UpdateProfile(UpdateInvestorProfileRequest request,CancellationToken cancellationToken)
    {

        var command=new UpdateInvestorProfileCommand(
            UserId,
            request.InvestorType!.Value,
            request.OrganizationName,
            request.CityId!.Value,
            request.AddressDetail
        );

        var result=await sender.Send(command,cancellationToken);
        return result.Match(_ => NoContent(),errors => this.ToProblem(errors));
    }

    [HttpGet]
    [Authorize(Roles =$"{AppRoles.Admin}, {AppRoles.Farmer}")]

    public async Task<IActionResult> GetInvestors([FromQuery] GetInvestorsRequest request,CancellationToken cancellationToken)
    {
        var query=new GetInvestorsQuery(request.PageNumber,request.PageSize,request.CityId,request.InvestorType);
        var result = await sender.Send(query,cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }
   
   [HttpGet("me")]
   [Authorize(Roles =AppRoles.Investor)]

   public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetInvestorProfileQuery(UserId),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles =$"{AppRoles.Admin}, {AppRoles.Farmer}")]

    
    public async Task<IActionResult> GetById(Guid id,CancellationToken cancellationToken)
    {
        var result=await sender.Send(new GetInvestorProfileByIdQuery(id),cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }   
}