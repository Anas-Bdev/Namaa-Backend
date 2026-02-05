using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Namaa.Application.Abstractions.Authentication;
using Namaa.Application.Abstractions.Security;
using Namaa.Application.Authentication.Dtos;
using Namaa.Domain.Common.Results;
using Namaa.Domain.Identity;
using Namaa.Infrastructure.Identity;

namespace Namaa.Infrastructure.Authentication;

public sealed class AuthService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,
  ITokenService tokenService) : IAuthService
{
    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
      var user=await userManager.FindByEmailAsync(request.Email);
      if(user is null)
      return Error.Validation("User.InvalidCredentials","Invalid credentials");
      var result=await signInManager.CheckPasswordSignInAsync(user,request.Password,true);
      if(result.IsLockedOut)
      return Error.Validation("User.LockedOut","Account is locked.  Try again later.");
     if(!result.Succeeded)
     return Error.Validation("User.InvalidCredentials","Invalid credentials");
     var roles=await userManager.GetRolesAsync(user);
     var identity=new UserIdentity(user.Id,user.Email!,user.UserName!);
     var token=await tokenService.CreateAccessTokenAsync(identity,roles,ct);
     return new AuthResponse
     {
         AccessToken=token.AccessToken,
         ExpiresAt=token.ExpiresAt,
         UserId=user.Id,
         Email=user.Email!,
         Roles=roles.ToList(),
         UserName=user.UserName
     };
    }
}