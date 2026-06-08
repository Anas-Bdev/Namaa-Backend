using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Admin.Commands.SuspendUser;
public sealed record SuspendUserCommand(Guid UserId, string Reason):IRequest<Result<Updated>>;