using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Consultations;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Consultations.Commands.AddConsultationMessage;
using Namaa.Application.Features.Consultations.Commands.CloseConsultation;
using Namaa.Application.Features.Consultations.Commands.RequestConsultation;
using Namaa.Application.Features.Consultations.Commands.UploadConsultationImage;
using Namaa.Application.Features.Consultations.Queries.GetAiPrimaryAdvice;
using Namaa.Application.Features.Consultations.Queries.GetAvailableConsultations;
using Namaa.Application.Features.Consultations.Queries.GetConsultationDetails;
using Namaa.Application.Features.Consultations.Queries.GetFarmerConsultations;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;
[Route("api/consultations")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public class ConsultationsController(ISender sender) : ControllerBase
{
   private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("my-consultations")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> GetMyConsultations(CancellationToken ct)
    {
        var result = await sender.Send(new GetFarmerConsultationQuery(UserId), ct);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }
    
    [HttpGet("available")]
    [Authorize(Roles = AppRoles.Expert)] 
    public async Task<IActionResult> GetAvailableConsultations(CancellationToken ct)
    {
        var result = await sender.Send(new GetAvailableConsultationsQuery(), ct);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = AppRoles.Farmer + "," + AppRoles.Expert)] 
    public async Task<IActionResult> GetConsultationDetails(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetConsultationDetailsQuery(id), ct);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> RequestConsultation([FromBody] RequestConsultationRequest request, CancellationToken ct)
    {
        var command = new RequestConsultationCommand(UserId, request.Title, request.Description, request.ImageUrl);
        
        var result = await sender.Send(command, ct);
        
        return result.Match(
            response => CreatedAtAction(nameof(GetConsultationDetails), new { id = response.Id }, response), 
            errors => this.ToProblem(errors));
    }

    [HttpPost("upload-image")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile formFile, CancellationToken ct)
    {
        var command = new UploadConsultationImageCommand(formFile);
        var result = await sender.Send(command, ct);
        
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpPost("{id:guid}/messages")]
    [Authorize(Roles = AppRoles.Farmer + "," + AppRoles.Expert)]
    public async Task<IActionResult> AddMessage(Guid id, [FromBody] AddConsultationMessageRequest request, CancellationToken ct)
    {
        var command = new AddConsultationMessageCommand(id, UserId, request.Content);
        var result = await sender.Send(command, ct);
        
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("{id:guid}/assign")]
    [Authorize(Roles = AppRoles.Expert)]
    public async Task<IActionResult> AssignExpert(Guid id, CancellationToken ct)
    {
        var command = new AssignExpertToConsultationCommand(id, UserId);
        var result = await sender.Send(command, ct);
        
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("{id:guid}/close")]
    [Authorize(Roles = AppRoles.Farmer + "," + AppRoles.Expert)]
    public async Task<IActionResult> CloseConsultation(Guid id, CancellationToken ct)
    {
        var command = new CloseConsultationCommand(id);
        var result = await sender.Send(command, ct);
        
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPost("ai-preview")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> GetAiPreview([FromBody] GetAiPrimaryAdviceRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new GetAiPrimaryAdviceQuery(request.Title,request.Description,request.ImageUrl), ct);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

}