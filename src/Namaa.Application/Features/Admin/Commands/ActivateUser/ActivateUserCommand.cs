using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Admin.Commands.ActivateUser;
public sealed record ActivateUserCommand(Guid UserId):IRequest<Result<Updated>>;