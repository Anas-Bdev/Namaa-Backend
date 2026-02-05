using Namaa.Application.Authentication.Dtos;

namespace Namaa.Application.Abstractions.Security;
public interface ITokenService
{
    Task<TokenResponse> CreateAccessTokenAsync(UserIdentity user,IEnumerable<string> roles,CancellationToken ct=default);
}