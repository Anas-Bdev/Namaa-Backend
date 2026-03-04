using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.ChangePassword;
public sealed record ChangePasswordCommand(string UserId,string CurrentPassword,string NewPassword):IRequest<Result<Success>>;