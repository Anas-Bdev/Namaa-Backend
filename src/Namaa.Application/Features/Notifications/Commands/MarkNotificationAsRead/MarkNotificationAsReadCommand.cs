using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Notifications.Commands.MarkNotificationAsRead;
public sealed record MarkNotificationAsReadCommand(Guid Id,Guid UserId):IRequest<Result<Updated>>;