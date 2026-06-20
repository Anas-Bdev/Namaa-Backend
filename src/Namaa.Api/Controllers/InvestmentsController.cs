using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Investments;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Investments.Commands.ApproveInvestorContribution;
using Namaa.Application.Features.Investments.Commands.CancelInvestmentProject;
using Namaa.Application.Features.Investments.Commands.CompleteInvestmentProject;
using Namaa.Application.Features.Investments.Commands.ConfirmContributionPayment;
using Namaa.Application.Features.Investments.Commands.CreateInvestmentProject;
using Namaa.Application.Features.Investments.Commands.CreateInvestorContribution;
using Namaa.Application.Features.Investments.Commands.RejectInvestorContribution;
using Namaa.Application.Features.Investments.Commands.StartInvestmentProject;
using Namaa.Application.Features.Investments.Commands.UpdateInvestmentProject;
using Namaa.Application.Features.Investments.Commands.UploadInvestmentProjectImage;
using Namaa.Application.Features.Investments.Commands.WithdrawInvestorContribution;
using Namaa.Application.Features.Investments.Queries.GetFundingInvestmentProjects;
using Namaa.Application.Features.Investments.Queries.GetInvestmentProjectById;
using Namaa.Application.Features.Investments.Queries.GetInvestmentProjectContributionsByIdj;
using Namaa.Application.Features.Investments.Queries.GetMyInvestmentProjects;
using Namaa.Application.Features.Investments.Queries.GetMyInvestorContributions;
using Namaa.Domain.Common.Constants;

namespace Namaa.Api.Controllers;
[Route("api/investments")]
[ApiController]
[Authorize]
public class InvestmentsController(ISender sender) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("projects/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInvestmentProjectByIdQuery(id), cancellationToken);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpGet("projects/my")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> GetMyProjects(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyInvestmentProjectsQuery(UserId), cancellationToken);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpGet("projects/{projectId:guid}/contributions")]
    [Authorize(Roles = AppRoles.Farmer)]

    public async Task<IActionResult> GetProjectContributions(Guid projectId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInvestmentProjectContributionsByIdQuery(projectId), cancellationToken);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

      

    [HttpGet("projects/funding")]
    [Authorize(Roles = AppRoles.Investor)]
    public async Task<IActionResult> GetFunding(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetFundingInvestmentProjectsQuery(), cancellationToken);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpPost("projects")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> Create(CreateInvestmentProjectRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateInvestmentProjectCommand(
            UserId,
            request.LandId,
            request.Title,
            request.Description,
            request.CoverImageUrl,
            request.RequiredAmount,
            request.MinimumInvestment,
            request.FundingDeadline,
            request.ExpectedRevenue,
            request.ExpectedCost,
            request.InvestorProfitSharePercentage,
            request.DurationInMonths,
            request.ExpectedStartDate,
            request.ExpectedEndDate
        );
        var result = await sender.Send(command, cancellationToken);
        return result.Match(
            response => CreatedAtAction(nameof(GetById), new { id = response.Id }, response),
            errors => this.ToProblem(errors));
    }

    [HttpPut("projects/{id:guid}")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> Update(Guid id, UpdateInvestmentProjectRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateInvestmentProjectCommand(
            id,
            request.Title,
            request.Description,
            request.CoverImageUrl,
            request.RequiredAmount,
            request.MinimumInvestment,
            request.FundingDeadline,
            request.ExpectedRevenue,
            request.ExpectedCost,
            request.InvestorProfitSharePercentage,
            request.DurationInMonths,
            request.ExpectedStartDate,
            request.ExpectedEndDate
        );
        var result = await sender.Send(command, cancellationToken);

        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

  
     

    [HttpPut("projects/{id:guid}/cancel")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CancelInvestmentProjectCommand(id), cancellationToken);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("projects/{id:guid}/start-progress")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> StartProgress(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new StartInvestmentProjectCommand(id), cancellationToken);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("projects/{id:guid}/complete")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> Complete(Guid id, CompleteInvestmentProjectRequest request, CancellationToken cancellationToken)
    {
        var command = new CompleteInvestmentProjectCommand(
            id,
            request.ActualRevenue,
            request.ActualCost
        );
        var result = await sender.Send(command, cancellationToken);

        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPost("projects/upload-image")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> UploadImage([FromForm] UploadInvestmentProjectImageRequest request, CancellationToken cancellationToken)
    {
        var command = new UploadInvestmentProjectImageCommand(request.FormFile);
        var result = await sender.Send(command, cancellationToken);

        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpPost("projects/{projectId:guid}/contributions")]
    [Authorize(Roles = AppRoles.Investor)]
    public async Task<IActionResult> CreateContribution(Guid projectId, CreateInvestorContributionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateInvestorContributionCommand(projectId,UserId,request.Amount);
        var result = await sender.Send(command, cancellationToken);

        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }
    
    [HttpGet("contributions/my")]
    [Authorize(Roles = AppRoles.Investor)]
    public async Task<IActionResult> GetMyContributions(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyInvestorContributionsQuery(UserId), cancellationToken);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpPut("contributions/{id:guid}/approve")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> ApproveContribution(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new ApproveInvestorContributionCommand(id), cancellationToken);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }
    

    [HttpPut("contributions/{id:guid}/reject")]
    [Authorize(Roles = AppRoles.Farmer)]
    public async Task<IActionResult> RejectContribution(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RejectInvestorContributionCommand(id), cancellationToken);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("projects/{projectId:guid}/contributions/{contributionId:guid}/withdraw")]
    [Authorize(Roles = AppRoles.Investor)]
    public async Task<IActionResult> WithdrawContribution(Guid projectId, Guid contributionId, CancellationToken cancellationToken)
    {
        var command = new WithdrawInvestorContributionCommand(projectId, contributionId, UserId);
        var result = await sender.Send(command, cancellationToken);
        
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    [HttpPut("projects/{projectId:guid}/contributions/{contributionId:guid}/confirm-payment")]
    [Authorize(Roles = AppRoles.Investor)]
    public async Task<IActionResult> ConfirmContributionPayment(Guid projectId, Guid contributionId, CancellationToken cancellationToken)
    {
        var command = new ConfirmContributionPaymentCommand(projectId, contributionId);
        var result = await sender.Send(command, cancellationToken);
        
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

    
}