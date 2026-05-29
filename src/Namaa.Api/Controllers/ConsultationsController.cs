using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Consultation;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Consultations.Commands.CloseConsultation;
using Namaa.Application.Features.Consultations.Commands.CreateConsultation;
using Namaa.Application.Features.Consultations.Commands.RespondToConsultation;
using Namaa.Application.Features.Consultations.Queries.GetAllConsultations;
using Namaa.Application.Features.Consultations.Queries.GetConsultationById;
using Namaa.Application.Features.Consultations.Queries.GetMyConsultations;
using Namaa.Application.Features.Consultations.Queries.GetMyResponses;
using Namaa.Domain.Common.Constants;
using Namaa.Domain.Enums;

namespace Namaa.Api.Controllers;

[Route("api/consultations")]
[ApiController]
[Authorize]
public class ConsultationsController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> CreateConsultation(
        [FromBody] CreateConsultationRequest request,
        CancellationToken ct)
    {
        var command = new CreateConsultationCommand(
            UserId,
            request.Title,
            request.Description,
            request.ImageUrl,
            request.Location
        );
        var result = await sender.Send(command, ct);
        return result.Match(c => Ok(c), errors => this.ToProblem(errors));
    }

    [HttpGet]
    [Authorize(Roles = AppRoles.Expert)]
    public async Task<IActionResult> GetAllConsultations(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] ConsultationStatus? status = null,
        CancellationToken ct = default)
    {
        var query = new GetAllConsultationsQuery(pageNumber, pageSize, status);
        var result = await sender.Send(query, ct);
        return result.Match(c => Ok(c), errors => this.ToProblem(errors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetConsultationById(Guid id, CancellationToken ct)
    {
        var query = new GetConsultationByIdQuery(id);
        var result = await sender.Send(query, ct);
        return result.Match(c => Ok(c), errors => this.ToProblem(errors));
    }

    [HttpGet("my-consultations")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> GetMyConsultations(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = new GetMyConsultationsQuery(UserId, pageNumber, pageSize);
        var result = await sender.Send(query, ct);
        return result.Match(c => Ok(c), errors => this.ToProblem(errors));
    }

    [HttpPost("{id}/respond")]
    [Authorize(Roles = AppRoles.Expert)]
    public async Task<IActionResult> RespondToConsultation(
        Guid id,
        [FromBody] RespondToConsultationRequest request,
        CancellationToken ct)
    {
        var command = new RespondToConsultationCommand(id, UserId, request.Message);
        var result = await sender.Send(command, ct);
        return result.Match(c => Ok(c), errors => this.ToProblem(errors));
    }

    [HttpPut("{id}/close")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> CloseConsultation(Guid id, CancellationToken ct)
    {
        var command = new CloseConsultationCommand(id, UserId);
        var result = await sender.Send(command, ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpGet("my-responses")]
    [Authorize(Roles = AppRoles.Expert)]
    public async Task<IActionResult> GetMyResponses(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = new GetMyResponsesQuery(UserId, pageNumber, pageSize);
        var result = await sender.Send(query, ct);
        return result.Match(c => Ok(c), errors => this.ToProblem(errors));
    }
}