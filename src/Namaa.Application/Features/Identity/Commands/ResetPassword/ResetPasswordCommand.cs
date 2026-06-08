using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.ResetPassword;
public sealed record ResetPasswordCommand(string Email,string ResetCode,string NewPassword):IRequest<Result<Success>>;