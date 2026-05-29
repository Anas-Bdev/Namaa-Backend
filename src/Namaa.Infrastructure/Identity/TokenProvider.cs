using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Namaa.Application.Common.Interfaces;
using Namaa.Application.Features.Identity.Dtos;
using Namaa.Domain.Common.Results;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Namaa.Domain.Identity;
using Namaa.Application.Features.Account.Dtos;

namespace Namaa.Infrastructure.Identity;

public class TokenProvider(IConfiguration configuration, IAppDbContext context) : ITokenProvider
{
    public async Task<Result<TokenResponse>> GenerateJwtTokenAsync(AppUserDto user,bool rememberMe=false, CancellationToken ct = default)
    {
        var tokenResult = await CreateAsync(user, rememberMe,ct);

        if (tokenResult.IsError)
        {
            return tokenResult.Errors;
        }

        return tokenResult.Value;
    }

    private async Task<Result<TokenResponse>> CreateAsync(AppUserDto user,bool rememberMe, CancellationToken ct)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var key = jwtSettings["Secret"];
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name,$"{user.FirstName} {user.LastName ?? string.Empty}".Trim()),
            new(ClaimTypes.Role, user.Role)
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(descriptor);
         var refreshTokenExpiryDate=rememberMe? DateTimeOffset.UtcNow.AddDays(7) : DateTimeOffset.UtcNow.AddDays(1);
        await context.RefreshTokens
            .Where(rt => rt.UserId == user.UserId)
            .ExecuteDeleteAsync(ct);

        var refreshTokenResult = RefreshToken.Create(
            Guid.NewGuid(), 
            GenerateRefreshToken(), 
            user.UserId, 
            refreshTokenExpiryDate);

        if (!refreshTokenResult.IsSuccess)
        {
            return refreshTokenResult.Errors;
        }

        var refreshToken = refreshTokenResult.Value;
        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync(ct);

        return new TokenResponse
        {
            AccessToken = tokenHandler.WriteToken(securityToken),
            RefreshToken = refreshToken.Token,
            ExpiresOnUtc = expires
        };
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!)),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || 
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return principal;
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}