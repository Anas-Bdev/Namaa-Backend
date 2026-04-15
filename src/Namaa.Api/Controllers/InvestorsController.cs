using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Investor;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Investors.Commands.CreateProfile;
using Namaa.Application.Features.Investors.Commands.UpdateProfile;
using Namaa.Application.Features.Investors.Queries.GetInvestorProfile;
using Namaa.Application.Features.Investors.Queries.GetInvestors;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvestorsController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("create-profile")]
    [Authorize(Roles = AppRoles.Investor)]
    public async Task<IActionResult> CreateProfile(CancellationToken ct)
    {
        var command = new CreateInvestorProfileCommand(UserId);
        var result = await sender.Send(command, ct);
        return result.Match(_ => Created(), errors => this.ToProblem(errors));
    }

    [HttpPut("update-profile")]
    [Authorize(Roles = AppRoles.Investor)]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] UpdateInvestorProfileRequest request,
        CancellationToken ct)
    {
        var command = new UpdateInvestorProfileCommand(
            UserId,
            request.OrganizationName,
            request.CompanyName,
            request.CityId,
            request.AddressDetail
        );
        var result = await sender.Send(command, ct);
        return result.Match(profile => Ok(profile), errors => this.ToProblem(errors));
    }

    [HttpGet("my-profile")]
    [Authorize(Roles = AppRoles.Investor)]
    public async Task<IActionResult> GetMyProfile(CancellationToken ct)
    {
        var query = new GetInvestorProfileQuery(UserId);
        var result = await sender.Send(query, ct);
        return result.Match(profile => Ok(profile), errors => this.ToProblem(errors));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetInvestors(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? cityId = null,
        CancellationToken ct = default)
    {
        var query = new GetInvestorsQuery(pageNumber, pageSize, cityId);
        var result = await sender.Send(query, ct);
        return result.Match(investors => Ok(investors), errors => this.ToProblem(errors));
    }
}