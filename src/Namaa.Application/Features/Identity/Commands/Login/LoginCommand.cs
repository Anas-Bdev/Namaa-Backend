using MediatR;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.Login;
public sealed record LoginCommand(string Email,string Password,bool RememberMe):IRequest<Result<TokenResponse>>;