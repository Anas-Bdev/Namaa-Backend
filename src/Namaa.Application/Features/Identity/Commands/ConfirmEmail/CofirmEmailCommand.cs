using MediatR;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.ConfirmEmail;
public sealed record ConfirmEmailCommand(string UserId,string Token):IRequest<Result<TokenResponse>>;