using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Investment;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Investments.Commands.CreateContribution;
using Namaa.Application.Features.Investments.Commands.CreateProject;
using Namaa.Application.Features.Investments.Commands.DeleteProject;
using Namaa.Application.Features.Investments.Commands.RespondToContribution;
using Namaa.Application.Features.Investments.Commands.UpdateProject;
using Namaa.Application.Features.Investments.Queries.GetAllProjects;
using Namaa.Application.Features.Investments.Queries.GetMyContributions;
using Namaa.Application.Features.Investments.Queries.GetMyProjects;
using Namaa.Application.Features.Investments.Queries.GetProjectById;
using Namaa.Domain.Common.Constants;
using Namaa.Domain.Enums;

namespace Namaa.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InvestmentsController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    private string UserRole => User.FindFirstValue(ClaimTypes.Role)!;

    // ========== Projects ==========

    [HttpPost("projects")]
    [Authorize(Roles = $"{AppRoles.Farmer},{AppRoles.Investor}")]
    public async Task<IActionResult> CreateProject(
        [FromBody] CreateInvestmentProjectRequest request,
        CancellationToken ct)
    {
        var creatorRole = UserRole == AppRoles.Farmer
            ? ProjectCreatorRole.Farmer
            : ProjectCreatorRole.Investor;

        var command = new CreateInvestmentProjectCommand(
            UserId,
            creatorRole,
            request.Title,
            request.Description,
            request.RequiredAmount,
            request.ExpectedProfit,
            request.SharePercentage
        );
        var result = await sender.Send(command, ct);
        return result.Match(project => Ok(project), errors => this.ToProblem(errors));
    }

    [HttpPut("projects/{id}")]
    [Authorize(Roles = $"{AppRoles.Farmer},{AppRoles.Investor}")]
    public async Task<IActionResult> UpdateProject(
        Guid id,
        [FromBody] UpdateInvestmentProjectRequest request,
        CancellationToken ct)
    {
        var command = new UpdateInvestmentProjectCommand(
            id,
            UserId,
            request.Title,
            request.Description,
            request.RequiredAmount,
            request.ExpectedProfit,
            request.SharePercentage
        );
        var result = await sender.Send(command, ct);
        return result.Match(project => Ok(project), errors => this.ToProblem(errors));
    }

    [HttpDelete("projects/{id}")]
    [Authorize(Roles = $"{AppRoles.Farmer},{AppRoles.Investor}")]
    public async Task<IActionResult> DeleteProject(Guid id, CancellationToken ct)
    {
        var command = new DeleteInvestmentProjectCommand(id, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpGet("my-projects")]
    [Authorize(Roles = $"{AppRoles.Farmer},{AppRoles.Investor}")]
    public async Task<IActionResult> GetMyProjects(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = new GetMyProjectsQuery(UserId, pageNumber, pageSize);
        var result = await sender.Send(query, ct);
        return result.Match(projects => Ok(projects), errors => this.ToProblem(errors));
    }

    [HttpGet("projects")]
    public async Task<IActionResult> GetAllProjects(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] ProjectCreatorRole? creatorRole = null,
        [FromQuery] ProjectStatus? status = null,
        CancellationToken ct = default)
    {
        var query = new GetAllProjectsQuery(pageNumber, pageSize, creatorRole, status);
        var result = await sender.Send(query, ct);
        return result.Match(projects => Ok(projects), errors => this.ToProblem(errors));
    }

    [HttpGet("projects/{id}")]
    public async Task<IActionResult> GetProjectById(Guid id, CancellationToken ct)
    {
        var query = new GetProjectByIdQuery(id);
        var result = await sender.Send(query, ct);
        return result.Match(project => Ok(project), errors => this.ToProblem(errors));
    }

    // ========== Contributions ==========

    [HttpPost("contributions")]
    [Authorize(Roles = $"{AppRoles.Farmer},{AppRoles.Investor}")]
    public async Task<IActionResult> CreateContribution(
        [FromBody] CreateContributionRequest request,
        CancellationToken ct)
    {
        var command = new CreateContributionCommand(
            request.ProjectId,
            UserId,
            request.Amount
        );
        var result = await sender.Send(command, ct);
        return result.Match(contribution => Ok(contribution), errors => this.ToProblem(errors));
    }

    [HttpPut("contributions/{id}/respond")]
    [Authorize(Roles = $"{AppRoles.Farmer},{AppRoles.Investor}")]
    public async Task<IActionResult> RespondToContribution(
        Guid id,
        [FromBody] RespondToContributionRequest request,
        CancellationToken ct)
    {
        var command = new RespondToContributionCommand(id, UserId, request.IsApproved);
        var result = await sender.Send(command, ct);
        return result.Match(contribution => Ok(contribution), errors => this.ToProblem(errors));
    }

    [HttpGet("my-contributions")]
    [Authorize(Roles = $"{AppRoles.Farmer},{AppRoles.Investor}")]
    public async Task<IActionResult> GetMyContributions(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = new GetMyContributionsQuery(UserId, pageNumber, pageSize);
        var result = await sender.Send(query, ct);
        return result.Match(contributions => Ok(contributions), errors => this.ToProblem(errors));
    }
} 