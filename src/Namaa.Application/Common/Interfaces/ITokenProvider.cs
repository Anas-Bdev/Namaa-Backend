using System.Security.Claims;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Results;

namespace Namaa.Application.Common.Interfaces;
public interface ITokenProvider
{
    Task<Result<TokenResponse>> GenerateJwtTokenAsync(AppUserDto user,CancellationToken ct=default);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}