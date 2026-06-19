using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Notifications.Dtos;
using Namaa.Application.Features.Notifications.Mapper;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Notifications.Queries.GetNotificatios;

public class GetNotificationsQueryHandler(IAppDbContext context) : IRequestHandler<GetNotificationsQuery, Result<List<NotificationDto>>>
{
    public async Task<Result<List<NotificationDto>>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await context.Notifications
            .AsNoTracking()
            .Where(n => n.UserId == request.UserId)
            .OrderByDescending(n => n.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return notifications.ToDtos();
    }
}