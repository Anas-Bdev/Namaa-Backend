using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Farmer;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Farmers.Commands.CreateProfile;
using Namaa.Application.Features.Farmers.Commands.UpdateProfile;
using Namaa.Application.Features.Farmers.Queries.GetFarmerProfile;
using Namaa.Application.Features.Farmers.Queries.GetFarmers;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FarmersController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("create-profile")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> CreateProfile(CancellationToken ct)
    {
        var command = new CreateFarmerProfileCommand(UserId);
        var result = await sender.Send(command, ct);
        return result.Match(_ => Created(), errors => this.ToProblem(errors));
    }

    [HttpPut("update-profile")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] UpdateFarmerProfileRequest request,
        CancellationToken ct)
    {
        var command = new UpdateFarmerProfileCommand(
            UserId,
            request.Description,
            request.CityId,
            request.AddressDetail,
            request.ExperienceLevel
        );
        var result = await sender.Send(command, ct);
        return result.Match(profile => Ok(profile), errors => this.ToProblem(errors));
    }

    [HttpGet("my-profile")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> GetMyProfile(CancellationToken ct)
    {
        var query = new GetFarmerProfileQuery(UserId);
        var result = await sender.Send(query, ct);
        return result.Match(profile => Ok(profile), errors => this.ToProblem(errors));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetFarmers(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? cityId = null,
        CancellationToken ct = default)
    {
        var query = new GetFarmersQuery(pageNumber, pageSize, cityId);
        var result = await sender.Send(query, ct);
        return result.Match(farmers => Ok(farmers), errors => this.ToProblem(errors));
    }
}