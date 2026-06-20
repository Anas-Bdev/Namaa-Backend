using System.ComponentModel.DataAnnotations;
using Namaa.Application.Features.Notifications.Dtos;
using Namaa.Domain.Notifications;

namespace Namaa.Application.Features.Notifications.Mapper;
public static class NotificationMapper
{
    public static NotificationDto ToDto(this Notification entity)
    {
        return new NotificationDto
        {
            Id=entity.Id,
            Title=entity.Title,
            Type=entity.Type,
            Message=entity.Message,
            IsRead=entity.IsRead,
            CreatedAt=entity.CreatedAtUtc,
            ReferencedId=entity.ReferencedId
        };
    }

    public static List<NotificationDto> ToDtos(this IEnumerable<Notification> entities)
    {
        return [..entities.Select(e => e.ToDto())];
    }
}