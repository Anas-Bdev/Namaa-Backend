using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Trader;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Traders.Commands.CreateProfile;
using Namaa.Application.Features.Traders.Commands.UpdateProfile;
using Namaa.Application.Features.Traders.Queries.GetTraderProfile;
using Namaa.Application.Features.Traders.Queries.GetTraders;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TradersController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("create-profile")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> CreateProfile(CancellationToken ct)
    {
        var command = new CreateTraderProfileCommand(UserId);
        var result = await sender.Send(command, ct);
        return result.Match(_ => Created(), errors => this.ToProblem(errors));
    }

    [HttpPut("update-profile")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] UpdateTraderProfileRequest request,
        CancellationToken ct)
    {
        var command = new UpdateTraderProfileCommand(
            UserId,
            request.BusinessName,
            request.BusinessType,
            request.PreferredCategories,
            request.CityId,
            request.AddressDetail
        );
        var result = await sender.Send(command, ct);
        return result.Match(profile => Ok(profile), errors => this.ToProblem(errors));
    }

    [HttpGet("my-profile")]
    [Authorize(Roles = AppRoles.Trader)]
    public async Task<IActionResult> GetMyProfile(CancellationToken ct)
    {
        var query = new GetTraderProfileQuery(UserId);
        var result = await sender.Send(query, ct);
        return result.Match(profile => Ok(profile), errors => this.ToProblem(errors));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetTraders(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? cityId = null,
        CancellationToken ct = default)
    {
        var query = new GetTradersQuery(pageNumber, pageSize, cityId);
        var result = await sender.Send(query, ct);
        return result.Match(traders => Ok(traders), errors => this.ToProblem(errors));
    }
}