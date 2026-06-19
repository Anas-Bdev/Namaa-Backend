using MediatR;
using Namaa.Application.Features.Notifications.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Notifications.Queries.GetNotificatios;
public sealed record GetNotificationsQuery(Guid UserId):IRequest<Result<List<NotificationDto>>>;