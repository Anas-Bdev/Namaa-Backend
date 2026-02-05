using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Namaa.Application.Abstractions.Security;
using Namaa.Application.Authentication.Dtos;

namespace Namaa.Infrastructure.Authentication;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public async Task<TokenResponse> CreateAccessTokenAsync(UserIdentity user, IEnumerable<string> roles, CancellationToken ct = default)
    {
     var jwtSettings=configuration.GetSection("JwtSettings");
     var issuer=jwtSettings["Issuer"];
     var audience=jwtSettings["Audience"];
     var key=jwtSettings["Secret"];
     var expires=DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!));
     var claims=new List<Claim>
     {
         new(JwtRegisteredClaimNames.Sub,user.UserId.ToString()),
         new(JwtRegisteredClaimNames.Email,user.Email)

     };
     foreach(var role in roles)
        {
        claims.Add(new(ClaimTypes.Role,role));
        }
     var descriptor=new SecurityTokenDescriptor
     {
        Subject=new ClaimsIdentity(claims),
         Issuer=issuer,
         Audience=audience,
         Expires=expires,
         SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
         SecurityAlgorithms.HmacSha256Signature)
         
     };
     var tokenHandler=new JwtSecurityTokenHandler();
     var securityToken=tokenHandler.CreateToken(descriptor);
     return new TokenResponse
     {
         AccessToken=tokenHandler.WriteToken(securityToken),
         ExpiresAt=expires
     };
    }
}