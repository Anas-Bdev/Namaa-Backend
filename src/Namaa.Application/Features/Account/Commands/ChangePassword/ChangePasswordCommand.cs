using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Account.Commands.ChangePassword;
public sealed record ChangePasswordCommand(string UserId,string CurrentPassword,string NewPassword):IRequest<Result<Updated>>;