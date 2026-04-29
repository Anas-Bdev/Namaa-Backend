using MediatR;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.ResendConfirmationEmail;
public sealed record ResendConfirmationEmailCommand(string Email):IRequest<Result<Success>>;