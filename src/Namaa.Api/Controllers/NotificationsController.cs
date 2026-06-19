using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Namaa.Api.Extensions;
using Namaa.Application.Features.Notifications.Commands.MarkNotificationAsRead;
using Namaa.Application.Features.Notifications.Queries.GetNotificatios;

namespace Namaa.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/notifications")]
public class NotificationsController(ISender sender) : ControllerBase
{
   private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetNotifications(CancellationToken ct)
    {
        var result = await sender.Send(new GetNotificationsQuery(UserId), ct);
        return result.Match(response => Ok(response), errors => this.ToProblem(errors));
    }

    [HttpPut("{notificationId:guid}/read")]
    public async Task<IActionResult> MarkAsRead(Guid notificationId, CancellationToken ct)
    {
        var result = await sender.Send(new MarkNotificationAsReadCommand(notificationId,UserId), ct);
        return result.Match(_ => NoContent(), errors => this.ToProblem(errors));
    }

}