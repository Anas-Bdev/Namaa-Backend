using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.ForgotPassword;
public sealed record ForgotPasswordCommand(string Email):IRequest<Result<Success>>;