using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Contracts.Requests.Recommendations;
using Namaa.Api.Extensions;
using Namaa.Domain.Common.Constants;
namespace Namaa.Api.Controllers;

[Route("api/recommendations")]
[ApiController]
[Authorize(Roles =AppRoles.Farmer)]
public class RecommendationsController(ISender sender) : ControllerBase
{
    [HttpPost("generate")]
    public async Task<IActionResult> GetCropRecommendations(GenerateCropRecommendationRequest request,CancellationToken cancellationToken)
    {
        var query=request.ToQuery();
        var result=await sender.Send(query,cancellationToken);
        return result.Match(response => Ok(response),errors => this.ToProblem(errors));
    }
}