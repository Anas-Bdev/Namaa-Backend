using MediatR;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Features.Identity.Commands.RefreshTokens;
public sealed record RefreshTokenCommand(string RefreshToken,string ExpiredAccessToken):IRequest<Result<TokenResponse>>;
