using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Admin.Queries.GetDashboardStatistics;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;
[Route("api/admin")]
[ApiController]
[Authorize(Roles = AppRoles.Admin)]
public class AdminDashboardController(ISender sender) : ControllerBase
{
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetStatistics(
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetDashboardStatisticsQuery(),
            cancellationToken);

        return result.Match(
            response => Ok(response),
            errors => this.ToProblem(errors));
    }
    }
